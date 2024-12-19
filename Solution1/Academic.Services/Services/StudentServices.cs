using Academic.Core.Entities;
using Academic.Core.Entities.ManyToManyEntities;
using Academic.Core.Enums;
using Academic.Core.Identitiy;
using Academic.Services.Helpers.Services;

namespace Academic.Services.Services
{
    public class StudentServices
        (IStudentRepository studentRepository, IUnitOfWork unitOfWork, 
        IPathRepository pathRepository,AccountServicesHelpers accountServices,
        IPathTasksRepository pathTasksRepository, IUserIdentityRepository identityRepository,
        IQuestionRepository questionRepository, IMapper mapper) 
        : IStudentServices
    {
        public async Task<Result<List<ModuleDto>>> GetAllModulesForStudent(int userId, int page = 1, int size = 10)
        {
            var result = await studentRepository.GetAllModules(userId, page, size);
            if (result.IsFailed)
                return Result.Fail(result.Errors);
            return mapper.Map<List<ModuleDto>>(result.Value);
        }

        public async Task<Result<List<PathDto>>> GetAllPathsForStudent(int userId, int page = 1, int size = 10)
        {
            var result = await pathRepository.GetPathsForUser(userId, page, size);
            if (result.IsFailed)
                return Result.Fail(result.Errors);
            return mapper.Map<List<PathDto>>(result.Value);
        }

        public async Task<Result<List<CertificationDto>>> GetCertifications(int userId, int page = 1, int size = 10)
        {
            var result = await pathTasksRepository.GetPathTasksCompletedForUser(userId, page, size);
            if (result.IsFailed)
                return Result.Fail(result.Errors);
            var certifications = new List<CertificationDto>();
            foreach(PathTaskUsers task in result.Value)
            {
                certifications.Add(new(task));
            }
            return Result.Ok(certifications);   
        }

        public async Task<Result<double>> GetProgressInPath(int userId, int pathId)
        {
            var result = await studentRepository.GetProgressInPath(userId, pathId);
            return result;
        }

        public async Task<Result<List<UserDto>>> GetTopInScore(int num)
        {
            var students = await studentRepository.GetTopInScore(num);
            return Result.Ok(mapper.Map<List<UserDto>>(students));
        }

        public async Task<Result> RollInModule(int moduleId, int userId)
        {
            var result = await studentRepository.RollInOrOutModule(moduleId: moduleId, studentId: userId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> RollInPath(int pathId, int userId)
        {
            var result = await studentRepository.RollInOrOutPath(pathId: pathId, studentId: userId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> SignUp(SignUpModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var userExists = await identityRepository.GetByEmail(model.Email);
            if (userExists != null)
                return EntityExistsError.Exists<User>();
            var user = new User()
            {
                Country = model.Country,
                Education = new()
                {
                    EducationalLevel = (EducationalLevel)Enum.Parse(typeof(EducationalLevel), model.EducationalLevel),
                    EducationalClass = (EducationalClass)Enum.Parse(typeof(EducationalClass), model.EducationalClass),
                },
                Email = model.Email,
                Phone = model.Phone,
                UserName = model.UserName,
                Role = ApplicationRole.Student
            };
            user.Salt = accountServices.CreateSalt();
            user.Password = accountServices.HashPasswordWithSalt(user.Salt, model.Password);
            var result = await identityRepository.CreateUser(user);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<double>> SolveQuestion(UserQuestionAnswerModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var answer = mapper.Map<UserQuestionAnswer>(model);
            var result = await questionRepository.SolveQuestion(answer);
            if (result.IsFailed)
                return Result.Fail(result.Errors);
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<SolvingTaskDto>> SolveTask(SolveTaskModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var TaskResult = await pathTasksRepository.GetTaskForPathById(model.TaskId);
            if (TaskResult.IsFailed)
                return Result.Fail(TaskResult.Errors);
            var totalPoints = 0d;
            var answers = new List<UserQuestionAnswer>();
            foreach(var answerModel in model.Answers)
            {
                var answer = mapper.Map<UserQuestionAnswer>(answerModel);
                answers.Add(answer);
                var result = await questionRepository.SolveQuestion(answer); ;
                if (result.IsFailed)
                    return Result.Fail(result.Errors);
                totalPoints += result.Value;
            }
            var userTask = new PathTaskUsers
            {
                Answers = answers,
                PathTaskId = model.TaskId,
                Score = totalPoints,
                UserId = model.UserId,
                HasCertification = ((totalPoints / TaskResult.Value.TotalPoints) * 100) > TaskResult.Value.MinPercentagesToCertify,
            };
            var savingAnswer = await pathTasksRepository.SolveTask(userTask);
            if (savingAnswer.IsFailed)
                return Result.Fail(savingAnswer.Errors);
            await unitOfWork.SaveChangesAsync();
            return Result.Ok(new SolvingTaskDto
            {
                HasCertification = userTask.HasCertification,
                Score = totalPoints,
                TaskPoints = TaskResult.Value.TotalPoints
            });
        }
    }
} 
