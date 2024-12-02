using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IModuleSectionsRepository
    {
        Task<int> GenerateNewModuleSectionInModule(ModuleSection moduleSection);
        Task<ModuleSection> DeleteModuleSection(int moduleSectionId);
        Task<ModuleSection> UpdateModuleSection(ModuleSection moduleSection);
        Task<ModuleSection> GetModuleSectionById(int moduleSectionId);
        Task<List<ModuleSection>> GetModuleSectionsInModule(int moduleId, int page = 1, int size = 15);
        Task<int> GetNumOfModuleSectionsInModule(int moduleId);

    }
}
