using Academic.Core.Abstractions;
using Academic.Core.Base;
using Academic.Core.Errors;
using Academic.Core.Identitiy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class UserIdentityRepository(ApplicationDbContext context) : IUserIdentityRepository
    {
        public async Task<Result> CreateUser(IdentityUser user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            var checkUser = await GetByEmail(user.Email);
            if (checkUser != null)
                return EntityExistsError.Exists<User>();
            await context.IdentityUsers.AddAsync(user);
            return Result.Ok();
        }

        public async Task<IdentityUser?> GetByEmail(string email)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IdentityUser?> GetById(int id)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityUser?> GetUserByCriteria(Func<IdentityUser, bool> criteria)
        {
            var account = await context.IdentityUsers.AsNoTracking().FirstOrDefaultAsync(x => criteria(x));
            if (account == null)
                throw new EntityNotFoundException(typeof(IdentityUser), nameof(GetUserByCriteria));
            return account;
        }

        public Task<int> UpdateAllWithFunc(Action<IdentityUser> action)
        {
            foreach (var user in context.IdentityUsers)
                action(user);
            return Task.FromResult(1);
        }

        public async Task<int> UpdateByCriteriaWithFunc(Func<IdentityUser, bool> criteria, Action<IdentityUser> action)
        {
            var user = await context.IdentityUsers.AsNoTracking().FirstOrDefaultAsync(u => criteria(u));
            if (user == null)
                throw new EntityNotFoundException(typeof(IdentityUser), nameof(UpdateByCriteriaWithFunc));
            action(user);
            return 1;
        }

        public async Task<IdentityUser> UpdateUser(IdentityUser identity)
        {
            await context.IdentityUsers.Where(u => u.Id == identity.Id).ExecuteUpdateAsync(user =>
            user
            .SetProperty(p => p.Password, identity.Password)
            .SetProperty(p => p.RefreshToken, identity.RefreshToken)
            .SetProperty(p => p.UserName, identity.UserName)
            .SetProperty(p => p.Role, identity.Role)
            .SetProperty(p => p.Phone, identity.Phone)
            .SetProperty(p => p.Salt, identity.Salt)
            );
            return identity;
        }
    }
}
