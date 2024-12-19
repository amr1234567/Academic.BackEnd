using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace Academic.Services.Services
{
    public class AdminServices
        (IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork,
        IUserIdentityRepository userIdentityRepository,
        IMapper mapper,
        IPathRepository pathRepository,
        IInstructorRepository instructorRepository,
        IIdentityTokenService tokenService,
        IOptions<AppDetailsHelper> options,
        AccountServicesHelpers accountServices,
        IEmailSender emailSender)
        : IAdminServices
    {
        private readonly AppDetailsHelper _appDetails = options.Value;
        public async Task<int> AcceptModule(int moduleId)
        {
            var module = await moduleRepository.GetModule(moduleId);
            if (module == null)
                throw new EntityNotFoundException(typeof(Module), moduleId);
            module.RejectModule();
            await moduleRepository.UpdateModule(module);
            await unitOfWork.SaveChangesAsync();
            return module.Id;
        }

        public async Task<int> AcceptPath(int pathId)
        {
            var path = await pathRepository.GetPath(pathId);
            if (path == null)
                throw new EntityNotFoundException(typeof(Path), pathId);
            path.AcceptPath();
            await pathRepository.UpdatePath(path);
            await unitOfWork.SaveChangesAsync();
            return path.Id;
        }

        public async Task<int> BlockInstructor(int id)
        {
            await instructorRepository.BlockInstructor(id);
            await unitOfWork.SaveChangesAsync();
            return id;
        }

        public async Task<int> DeleteInstructor(int id)
        {
            await instructorRepository.DeleteInstructor(id);
            await unitOfWork.SaveChangesAsync();
            return id;
        }

        public async Task<Result> GenerateAdmin(CreateAdminModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var checkUser = await userIdentityRepository.GetByEmail(model.Email);
            if (checkUser != null)
                return new EmailAlreadyExistError(model.Email);

            var salt = accountServices.CreateSalt();
            var admin = new Admin
            {
                Role = Core.Enums.ApplicationRole.Admin,
                Email = model.Email,
                Salt = salt,
                HashPassword = accountServices.HashPasswordWithSalt(salt, model.Password),
                UserName = model.UserName,
            };
            var result = await userIdentityRepository.CreateUser(admin);
            if (result.IsFailed)
                return Result.Fail(result.Errors);
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// TODO 
        /// create new instructor
        /// </summary>
        /// <param name="model">password and email</param>
        /// <returns>details of new instructor</returns>
        public async Task<Result> GenerateInstructor(CreateInstructorModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var checkUser = await userIdentityRepository.GetByEmail(model.Email);
            if (checkUser != null)
                return new EmailAlreadyExistError(model.Email);

            var instructor = mapper.Map<Instructor>(model);
            instructor.Role = Core.Enums.ApplicationRole.Instructor;

            var newToken = tokenService.GenerateEmailConfirmationToken(model.Email);
            instructor.ConfirmationToken = newToken;

            await instructorRepository.GenerateNewInstructor(instructor);

            var uri = $"{_appDetails.FrontEndLink}/{_appDetails.ConfirmEmailEndPointForInstructor}?" +
                $"email={model.Email}&token={newToken}";
            var emailBody = $"Please confirm your account by clicking this link: <br> <b>{uri}</b>";
            await emailSender.SendEmailAsync(model.Email, "Confirm Email", emailBody);
           
            await unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<List<PathDto>> GetAllPathsNeverGotResponse(int page = 1, int size = 10)
        {
            var paths = await pathRepository.GetPathsWithModulesAndInstructorWithCriteria(p => !p.IsAccepted == null, page, size);
            var pathsAsDto = paths.Select(p => new PathDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Difficulty = p.Difficulty,
                NumOfModules = p.NumOfModules,
                CreatedAt = p.CreatedAt,
                InstructorId = p.InstructorId,
                InstructorName = p.Instructor.UserName,
                Modules = p.Modules?.Select(m => new ModuleDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Difficulty = m.Difficulty,
                    Description = m.Description,
                    NumOfSections = m.NumOfSections,
                    ExpectedTimeToComplete = m.ExpectedTimeToComplete,
                    CreatedAt = m.CreatedAt,
                }).ToList() ?? []
            }).ToList();

            return pathsAsDto;
        }

        public async Task<int> RejectModule(int moduleId)
        {
            var module = await moduleRepository.GetModule(moduleId);
            if (module == null)
                throw new EntityNotFoundException(typeof(Module), moduleId);
            module.RejectModule();
            await moduleRepository.UpdateModule(module);
            await unitOfWork.SaveChangesAsync();
            return module.Id;
        }

        public async Task<int> RejectPath(int pathId)
        {
            var path = await pathRepository.GetPath(pathId);
            if (path == null)
                throw new EntityNotFoundException(typeof(Path), pathId);
            path.RejectPath();
            await pathRepository.UpdatePath(path);
            await unitOfWork.SaveChangesAsync();
            return path.Id;
        }

        public async Task<InstructorDto> UpdateInstructorDetails(UpdateInstructorForAdminModel model)
        {
            var user = await userIdentityRepository.GetById(model.Id);
            if (user == null || user is not Instructor instructor)
                throw new EntityNotFoundException(typeof(IdentityUser), model.Id);
            await instructorRepository.UpdateInstructorDetails(instructor);
            await userIdentityRepository.UpdateUser(instructor);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<InstructorDto>(instructor);
        }
    } 
}
