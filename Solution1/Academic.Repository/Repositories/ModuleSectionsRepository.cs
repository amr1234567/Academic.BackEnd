using Academic.Core.Abstractions;
using Academic.Core.Entities;
using Academic.Core.Errors;
using Academic.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class ModuleSectionsRepository : IModuleSectionsRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleSectionsRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Result> GenerateNewModuleSectionInModule(ModuleSection moduleSection)
        {
            if (moduleSection == null)
                return BadRequestError.Exists<ModuleSection>();
            await _context.ModuleSections.AddAsync(moduleSection);
            return Result.Ok();
        }
        public async Task<Result> DeleteModuleSection(int moduleSectionId)
        {
            var module = await _context.Modules.FindAsync(moduleSectionId);

            if (module != null)
            { 
                // Remove 
                _context.Modules.Remove(module);
                return Result.Ok();
            }

            return EntityNotFoundError.Exists<ModuleSection>(moduleSectionId);
        }

        public async Task<ModuleSection> GetModuleSectionById(int moduleSectionId)
        {
            var moduleSection = await _context
                .ModuleSections
                .FirstOrDefaultAsync(m => m.Id == moduleSectionId);

            if(moduleSection != null)
                return moduleSection;

            return null;
        }

        public async Task<List<ModuleSection>> GetModuleSectionsInModule(int moduleId, int page = 1, int size = 15)
        {
            // Validation 

            if(page <= 0)
                page = 1;
            if (size <= 0)
                size = 15;

            return await _context.ModuleSections
                .Where(m => m.Id == moduleId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetNumOfModuleSectionsInModule(int moduleId)
        {
            return await _context.ModuleSections
                .Where(m => m.Id == moduleId)
                .CountAsync();
        }

        public async Task<Result> UpdateModuleSection(ModuleSection moduleSection)
        {
            var newModuleSectionData = await _context.ModuleSections.FindAsync(moduleSection.Id);

            if (newModuleSectionData != null)
            {
                newModuleSectionData.Title = moduleSection.Title;
                newModuleSectionData.Body = moduleSection.Body;
                newModuleSectionData.QuizId = moduleSection.QuizId;
                newModuleSectionData.ModuleId = moduleSection.ModuleId;


                _context.ModuleSections.Update(newModuleSectionData);
                return Result.Ok();
            }

            return EntityNotFoundError.Exists<ModuleSection>(moduleSection.Id);
        }
    }
}
