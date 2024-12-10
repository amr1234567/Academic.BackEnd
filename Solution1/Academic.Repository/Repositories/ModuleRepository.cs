using Academic.Core.Errors;

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
        public async Task<Result> GenerateModule(Module module)
        {
            if (module == null) 
                return EntityNotFoundError.Exists(typeName: typeof(Module));
            await _context.Modules.AddAsync(module);
            return Result.Ok();
        }
        public async Task<Result> DeleteModule(int moduleId)
        {
            // Find module 
            var module = await _context.Modules.FindAsync(moduleId);

            if (module != null)
            {
                // Remove
               _context.Modules.Remove(module);
                return Result.Ok();
            }
            return EntityNotFoundError.Exists(typeName: typeof(Module), moduleId);
            // not found
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

        public async Task<Result> UpdateModule(Module module)
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
                return Result.Ok();
            }

            return EntityNotFoundError.Exists(typeName: typeof(Module), module.Id);
        }
    }
}
