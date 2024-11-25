using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    internal interface IUserIdentityRepository
    {
        Task<User> GetTopInScore(int num);
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);

    }
}
