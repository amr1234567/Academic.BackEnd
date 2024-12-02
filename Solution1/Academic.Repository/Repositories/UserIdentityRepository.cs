using Academic.Core.Abstractions;
using Academic.Core.Base;
using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class UserIdentityRepository(ApplicationDbContext context) : IUserIdentityRepository
    {
        public async Task<IdentityUser?> GetByEmail(string email)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IdentityUser?> GetById(int id)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

       
    }
}
