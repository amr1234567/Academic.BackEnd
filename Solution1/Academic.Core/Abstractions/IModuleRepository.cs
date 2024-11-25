using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    internal interface IModuleRepository
    {
        Task<int> GenerateModule(Module module);
        Task<Module> DeleteModule(int moduleId);
        Task<Module> GetModule(int moduleId);
        Task<List<Module>> GetModulesInPath(int pathId, int page = 1, int size = 10);
        Task<List<Module>> GetModules(int page = 1, int size = 10);
        Task<int> GetNumOfModulesInPath(int pathId);
        Task<Module> UpdateModule(Module module);
    }
}
