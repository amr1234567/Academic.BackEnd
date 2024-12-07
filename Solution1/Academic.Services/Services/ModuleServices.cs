using Academic.Core.Abstractions;
using Academic.Repository.Repositories;
using Academic.Services.Abstractions;
using Academic.Services.Models.Outputs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class ModuleServices : IModuleServices
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleSectionsRepository _moduleSectionsRepository;
        private readonly IMapper _mapper;

        public ModuleServices(IModuleRepository moduleRepository, IModuleSectionsRepository moduleSectionsRepository
            , IMapper mapper)
        {
            this._moduleRepository = moduleRepository;
            this._moduleSectionsRepository = moduleSectionsRepository;
            this._mapper = mapper;
        }
        public async Task<List<ModuleDto>> GetAllModules(int page = 1, int size = 10)
        {
            // get modules using ModuleRepository
            var modules = await _moduleRepository.GetModules(page, size);

            return _mapper.Map<List<ModuleDto>>(modules);
        }

        public async Task<List<ModuleDto>> GetAllModulesInPath(int pathId, int page = 1, int size = 10)
        {
            var modulePath = await _moduleRepository.GetModulesInPath(pathId, page, size);

            return _mapper.Map<List<ModuleDto>>(modulePath);
        }

        public async Task<ModuleDto> GetModuleById(int moduleId)
        {
            var module = await _moduleRepository.GetModule(moduleId);

            if(module != null)
                return _mapper.Map<ModuleDto>(module);

            return null;
        }

        public async Task<ModuleSectionDto> GetSectionById(int sectionId)
        {
             var modelSection = await _moduleSectionsRepository.GetModuleSectionById(sectionId);

            return _mapper.Map<ModuleSectionDto>(modelSection);
        }

        public async Task<List<ModuleSectionDto>> GetSectionsInModule(int moduleId, int page = 1, int size = 10)
        {
            var modelSections = await _moduleSectionsRepository
                .GetModuleSectionsInModule(moduleId, page, size);

            return _mapper.Map<List<ModuleSectionDto>>(modelSections);

        }
    }
}
