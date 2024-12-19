using Academic.Core.Entities;
using Academic.Core.Identitiy;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IStudentServices
    {
        Task<Result> SignUp(SignUpModel model);
        Task<Result> RollInPath(int pathId, int userId);
        Task<Result> RollInModule(int moduleId, int userId);
        Task<Result<double>> SolveQuestion(UserQuestionAnswerModel model);
        Task<Result<List<CertificationDto>>> GetCertifications(int userId, int page = 1, int size = 10);
        Task<Result<List<UserDto>>> GetTopInScore(int num);
        Task<Result<SolvingTaskDto>> SolveTask(SolveTaskModel model);
        Task<Result<List<PathDto>>> GetAllPathsForStudent(int userId, int page = 1, int size = 10);
        Task<Result<List<ModuleDto>>> GetAllModulesForStudent(int userId, int page = 1, int size = 10);
        Task<Result<double>> GetProgressInPath(int userId, int pathId);
    }
}
