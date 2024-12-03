using Academic.Services.Models;
using System.Security.Claims;

namespace Academic.Services.Abstractions
{
    public interface ITokenServices
    {
        Task<TokenModel> GenerateNewTokenModel(int userId, IEnumerable<Claim>? claims = null);
        Task<TokenModel> RefreshTheToken(string refreshToken, string token);
        Task<bool> RevokeAllTokens();
        Task<bool> RevokeToken(string refreshToken);
        Task<bool> RevokeTokenWithUserId(int userId);
    }
}