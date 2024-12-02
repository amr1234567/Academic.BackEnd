using Academic.Core.Abstractions;
using Academic.Core.Entities;
using Academic.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        // DI
        public ModuleRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> GenerateModule(Module module)
        {
            await _context.Modules.AddAsync(module);
            return await _context.SaveChangesAsync();
        }
        public async Task<Module> DeleteModule(int moduleId)
        {
            // Find module 
            var module = await _context.Modules.FindAsync(moduleId);

            if (module != null)
            {
                // Remove
               _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
                //return module;
            }
            // not found
            return null; 
        }

        public async Task<Module> GetModule(int moduleId)
        {
            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == moduleId);

            if (module != null) 
                 return module;

            return null;
        }

        public async Task<List<Module>> GetModules(int page = 1, int size = 10)
        {
            if(page <= 0)
                page = 1;
            if(size <= 10)
                size = 10;

            return await _context.Modules
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<Module>> GetModulesInPath(int pathId, int page = 1, int size = 10)
        {
            if (page <= 0)
                page = 1;
            if (size <= 10)
                size = 10;

            return await _context.Modules
                .Where(m => m.Id == pathId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetNumOfModulesInPath(int pathId)
        {
            return await _context.Modules
                .Where (m => m.Id == pathId)
                .CountAsync();
        }

        public async Task<Module> UpdateModule(Module module)
        {
            var newModuleData = await _context.Modules.FindAsync(module.Id);

            if (newModuleData == null)
            {
                newModuleData.Title = module.Title;
                newModuleData.Description = module.Description;
                newModuleData.Difficulty = module.Difficulty;
                newModuleData.NumOfSections = module.NumOfSections;
                newModuleData.ExpectedTimeToComplete = module.ExpectedTimeToComplete;
                newModuleData.CreatedAt = module.CreatedAt; 
                newModuleData.PathId = module.PathId;
                // update 
                _context.Modules.Update(newModuleData);
                await _context.SaveChangesAsync();
                return newModuleData;
            }

            return null;
        }
    }
}
