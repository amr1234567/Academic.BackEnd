using Academic.Core.Abstractions;
using Academic.Core.Entities.ManyToManyEntities;
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
        public async Task<int> RollInOrOutModule(int studentId, int moduleId, bool rollIn = true)
        {
            if (rollIn)
            {
                var student = await context.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentId)
                ?? throw new EntityNotFoundException(typeof(User), studentId);
                var module = await context.Modules.AsNoTracking().FirstOrDefaultAsync(s => s.Id == moduleId)
                    ?? throw new EntityNotFoundException(typeof(Module), moduleId);
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
                var studentModule = await context.ModuleUsers.FirstOrDefaultAsync(m => m.UserId == studentId && m.ModuleId == moduleId)
                    ?? throw new EntityNotFoundException(typeof(ModuleUsers), moduleId);
                context.ModuleUsers.Remove(studentModule);
            }
            return 1;
        }

        public async Task<int> RollInOrOutPath(int studentId, int pathId, bool rollIn = true)
        {
            if (rollIn)
            {
                var student = await context.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentId)
                ?? throw new EntityNotFoundException(typeof(User), studentId);
                var path = await context.Paths.AsNoTracking().FirstOrDefaultAsync(s => s.Id == pathId)
                    ?? throw new EntityNotFoundException(typeof(EducationalPath), pathId);
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
                var pathUser = await context.PathUsers.FirstOrDefaultAsync(m => m.UserId == studentId && m.PathId == pathId)
                    ?? throw new EntityNotFoundException(typeof(PathUsers), pathId);
                context.PathUsers.Remove(pathUser);
            }
            return 1;
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
    }
}
