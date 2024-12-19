using Academic.Core.Abstractions;
using Academic.Core.Entities.ManyToManyEntities;
using Academic.Core.Errors;
using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class StudentRepository(ApplicationDbContext context) : IStudentRepository
    {
        public async Task<Result> RollInOrOutModule(int studentId, int moduleId, bool rollIn = true)
        {
            if (rollIn)
            {
                var student = await context.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentId);
                if (student == null)
                    return EntityNotFoundError.Exists<User>(studentId);
                var module = await context.Modules.AsNoTracking().FirstOrDefaultAsync(s => s.Id == moduleId);
                if (module == null)
                    return EntityNotFoundError.Exists<Module>(moduleId);
                var newStudentModule = new ModuleUsers
                {
                    UserId = studentId,
                    User = student,
                    Module = module,
                    ModuleId = moduleId,
                    ProgressPresented = 0
                };
                await context.ModuleUsers.AddAsync(newStudentModule);
            }
            else
            {
                var studentModule = await context.ModuleUsers.AsNoTracking()
                    .FirstOrDefaultAsync(m => m.UserId == studentId && m.ModuleId == moduleId);
                 if (studentModule == null)
                    return EntityNotFoundError.Exists<ModuleUsers>(moduleId);
                context.ModuleUsers.Remove(studentModule);
            }
            return Result.Ok();
        }

        public async Task<Result> RollInOrOutPath(int studentId, int pathId, bool rollIn = true)
        {
            if (rollIn)
            {
                var student = await context.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentId);
                if (student == null)
                    return EntityNotFoundError.Exists<User>(studentId);
                var path = await context.Paths.AsNoTracking().FirstOrDefaultAsync(s => s.Id == pathId);
                if (path == null)
                    return EntityNotFoundError.Exists<EducationalPath>(pathId);
                var newStudentPath = new PathUsers
                {
                    UserId = studentId,
                    User = student,
                    Path = path,
                    IsCompleted = false,
                    PathId = pathId,
                    NumberOfCompletedModules = await context.ModuleUsers.AsNoTracking().CountAsync(m => m.UserId == studentId && m.ProgressPresented == 100)
                };
                await context.PathUsers.AddAsync(newStudentPath);
            }
            else
            {
                var pathUser = await context.PathUsers.FirstOrDefaultAsync(m => m.UserId == studentId && m.PathId == pathId);
                if (pathUser == null)
                    return EntityNotFoundError.Exists<PathUsers>(studentId);
                context.PathUsers.Remove(pathUser);
            }
            return Result.Ok();
        }

        public async Task<int> UpdateDetails(User user)
        {
            return await context.Users.Where(u => u.Id == user.Id)
                .ExecuteUpdateAsync(userDb =>
                userDb
                    .SetProperty(u => u.IsLocked, user.IsLocked)
                    .SetProperty(u => u.Points, user.Points)
                    .SetProperty(u => u.UserName, user.UserName)
                    .SetProperty(u => u.Country, user.Country)
                    .SetProperty(u => u.Education, user.Education)
                    .SetProperty(u => u.Password, user.Password)
                    .SetProperty(u => u.Phone, user.Phone)
                );
        }

        public async Task<List<User>> GetTopInScore(int num)
        {
            return await context.Users.AsNoTracking().OrderByDescending(u=>u.Points).Take(num).ToListAsync();
        }

        public async Task<Result<List<Module>>> GetAllModules(int studentId, int page = 1, int size = 10)
        {
            var student = await context.Users.AsNoTracking().Include(u => u.Modules)
                .FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
                return EntityNotFoundError.Exists(typeof(User), studentId);
            if (student.Modules is null)
                return EntityNotFoundError.Exists<Module>("No Modules");
            return student.Modules.Skip((page - 1) * size).Take(size).ToList();
        }

        public async Task<Result<List<EducationalPath>>> GetAllPaths(int studentId, int page = 1, int size = 10)
        {
            var student = await context.Users.AsNoTracking().Include(u => u.Paths)
               .FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
                return EntityNotFoundError.Exists(typeof(User), studentId);
            if (student.Paths is null)
                return EntityNotFoundError.Exists<EducationalPath>("No Paths");
            return student.Paths.Skip((page - 1) * size).Take(size).ToList();
        }

        public async Task<Result<double>> GetProgressInPath(int userId, int pathId)
        {
            var path = await context.PathUsers.AsNoTracking().Where(p => p.UserId == userId && p.PathId == pathId)
                                        .Include(p => p.Path).FirstOrDefaultAsync();
            if (path == null)
                return EntityNotFoundError.Exists<PathUsers>(userId);

            return path.IsCompleted ? 100.0 : ((path.NumberOfCompletedModules / path.Path.NumOfModules) * 100);
        }
    }
}
