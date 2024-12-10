using Academic.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IModuleRepository
    {
        Task<Result> GenerateModule(Module module);
        Task<Result> DeleteModule(int moduleId);
        Task<Module> GetModule(int moduleId);
        Task<List<Module>> GetModulesInPath(int pathId, int page = 1, int size = 10);
        Task<List<Module>> GetModules(int page = 1, int size = 10);
        Task<int> GetNumOfModulesInPath(int pathId);
        Task<Result> UpdateModule(Module module);
    }
}
