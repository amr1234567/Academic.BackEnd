using Academic.Core.Entities;
using Academic.Core.Identitiy;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IStudentRepository
    {
        Task<List<User>> GetTopInScore(int num);
        Task<Result> RollInOrOutModule(int studentId, int moduleId, bool rollIn = true);
        Task<Result> RollInOrOutPath(int studentId, int pathId, bool rollIn = true);
        Task<int> UpdateDetails(User user);
        Task<Result<List<Module>>> GetAllModules(int studentId, int page = 1, int size = 10);
        Task<Result<List<EducationalPath>>> GetAllPaths(int studentId, int page = 1, int size = 10);
        Task<Result<double>> GetProgressInPath(int userId, int pathId);
    }
}
