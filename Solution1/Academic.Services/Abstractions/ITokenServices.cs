﻿using Academic.Services.Models.Outputs;
using System.Security.Claims;

namespace Academic.Services.Abstractions
{
    public interface ITokenServices
    {
        Task<Result<TokenModel>> GenerateNewTokenModel(int userId, IEnumerable<Claim>? claims = null);
        Task<Result<TokenModel>> RefreshTheToken(string refreshToken, string token);
        Task<Result> RevokeAllTokens();
        Task<Result> RevokeToken(string refreshToken);
        Task<Result> RevokeTokenWithUserId(int userId);
    }
}