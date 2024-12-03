using Academic.Core.Entities;
using Academic.Core.Identitiy;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IStudentServices
    {
        Task<int> SignUp(SignUpModel model);
        Task<int> RollInPath(int pathId, int userId);
        Task<int> RollInModule(int moduleId, int userId);
        Task<int> SolveQuestion(UserQuestionAnswerModel model);
        Task<int> GetCertifications(int userId, int page = 1, int size = 10);
        Task<List<UserDto>> GetTopInScore(int num);
        Task<int> SolveTask(SolveTaskModel model);
        Task<SolvingQuestionAnswerDto> SolveQuestion(SolveQuestionModel model);
        Task<List<PathDto>> GetAllPathsForUser(int userId, int page = 1, int size = 10);
        Task<List<ModuleDto>> GetAllModulesForUser(int userId, int page = 1, int size = 10);
        Task<double> GetProgressInPath(int userId, int pathId);
    }
}
