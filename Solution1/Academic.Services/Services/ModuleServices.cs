using Academic.Services.Abstractions;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class ModuleServices : IModuleServices
    {
        public Task<List<ModuleDto>> GetAllModules(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<ModuleDto>> GetAllModulesInPath(int pathId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleDto> GetModuleById(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleSectionDto> GetSectionById(int sectionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ModuleSectionDto>> GetSectionsInModule(int moduleId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }
    }
}
