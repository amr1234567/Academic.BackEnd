using Academic.Core.Entities;
using Academic.Core.Errors;
using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class StudentServices
        (IStudentRepository studentRepository, IUnitOfWork unitOfWork, IModuleRepository moduleRepository
        , IQuestionRepository questionRepository, IMapper mapper) 
        : IStudentServices
    {
        public Task<List<ModuleDto>> GetAllModulesForStudent(int userId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<PathDto>> GetAllPathsForStudent(int userId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCertifications(int userId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetProgressInPath(int userId, int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetTopInScore(int num)
        {
            throw new NotImplementedException();
        }

        public Task<int> RollInModule(int moduleId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RollInPath(int pathId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SignUp(SignUpModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> SolveQuestion(UserQuestionAnswerModel model)
        {
            var answer = mapper.Map<UserQuestionAnswer>(model);
            var result = await questionRepository.SolveQuestion(answer);
            return result;
        }

        public Task<int> SolveTask(SolveTaskModel model)
        {
            throw new NotImplementedException();
        }
    }
}
