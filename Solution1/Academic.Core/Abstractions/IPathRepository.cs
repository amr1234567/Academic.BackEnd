using Academic.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IPathRepository
    {
        Task<Result> GenerateNewPath(EducationalPath path);
        Task<Result> DeletePath(int pathId);
        Task<Result> UpdatePath(EducationalPath path);
      
        Task<EducationalPath> GetPath(int pathId);
        Task<List<EducationalPath>> GetPaths(int page = 1, int size = 15);
        Task<List<EducationalPath>> GetPathsForUser(int userId, int page = 1, int size = 15);
        Task<List<EducationalPath>> GetPathsForInstructor(int instructorId, int page = 1, int size = 15);
        Task<List<EducationalPath>> GetPathsWithCriteria(Func<EducationalPath,bool> criteria, int page = 1, int size = 15);
        Task<List<EducationalPath>> GetPathsWithModulesAndInstructorWithCriteria(Func<EducationalPath, bool> value, int page = 1, int size = 15);
        Task<List<EducationalPath>> GetPathsWithModules(int page = 1, int size = 15);

    }
}
