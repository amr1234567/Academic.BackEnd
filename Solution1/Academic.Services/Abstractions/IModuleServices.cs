using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IModuleServices
    {
        Task<List<ModuleDto>> GetAllModulesInPath(int pathId, int page = 1, int size = 10);
        Task<List<ModuleDto>> GetAllModules(int page = 1, int size = 10);
        Task<ModuleDto> GetModuleById(int moduleId);
        Task<List<ModuleSectionDto>> GetSectionsInModule(int moduleId, int page = 1, int size = 10);
        Task<ModuleSectionDto> GetSectionById(int sectionId);

    }
}
