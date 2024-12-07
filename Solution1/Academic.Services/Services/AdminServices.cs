

using Academic.Core.Entities;
using Academic.Core.Identitiy;
using Academic.Services.Models.Inputs;
using System.IO;

namespace Academic.Services.Services
{
    public class AdminServices
        (IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork,
        IUserIdentityRepository userIdentityRepository,
        IMapper mapper,
        IPathRepository pathRepository,
        IInstructorRepository instructorRepository)
        : IAdminServices
    {
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

        public async Task<CreatingInstructorDto> GenerateInstructor(CreateInstructorModel model)
        {
            var instructor = mapper.Map<Instructor>(model);
            var id = await instructorRepository.GenerateNewInstructor(instructor);
            await unitOfWork.SaveChangesAsync();
            return new CreatingInstructorDto(id);
        }

        // TODO 
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
