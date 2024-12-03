using Academic.Core.Base;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IIdentityUserServices
    {
        Task<UserDto> GetUserData(int userId);
        Task<TokenModel> SignIn(string email, string password);
        Task<int> SignOut(string email);
    }
}
