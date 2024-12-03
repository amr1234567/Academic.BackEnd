using Academic.Services.Abstractions;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class IdentityUserServices : IIdentityUserServices
    {
        public Task<UserDto> GetUserData(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<TokenModel> SignIn(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<int> SignOut(string email)
        {
            throw new NotImplementedException();
        }
    }
}
