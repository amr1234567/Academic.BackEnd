using Academic.Core.Base;
using Academic.Services.Models.Outputs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IIdentityUserServices
    {
        Task<Result<UserDto>> GetUserData(int userId);
        Task<Result<TokenModel>> SignIn(string email, string password);
        Task<Result> SignOut(string email);
    }
}
