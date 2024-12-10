using Academic.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IModuleSectionsRepository
    {
        Task<Result> GenerateNewModuleSectionInModule(ModuleSection moduleSection);
        Task<Result> DeleteModuleSection(int moduleSectionId);
        Task<Result> UpdateModuleSection(ModuleSection moduleSection);
        Task<ModuleSection> GetModuleSectionById(int moduleSectionId);
        Task<List<ModuleSection>> GetModuleSectionsInModule(int moduleId, int page = 1, int size = 15);
        Task<int> GetNumOfModuleSectionsInModule(int moduleId);

    }
}
