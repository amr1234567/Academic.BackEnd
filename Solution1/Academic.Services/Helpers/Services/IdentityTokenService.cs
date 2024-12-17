using Academic.Core.Helpers;
using Academic.Services.Helpers.Abstractions;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Academic.Services.Helpers.Services
{
    public class IdentityTokenService(IOptions<TokenHelperConfigurations> options) : IIdentityTokenService
    {
        private readonly TokenHelperConfigurations _configuration = options.Value;

        public string GenerateEmailConfirmationToken(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentException("Email cannot be null or empty.");

            var expirationTime = DateTime.UtcNow.AddMinutes(_configuration.TokenExpirationMinutes).ToString("o"); // ISO 8601 format

            var data = $"{userEmail}|{expirationTime}";

            // Generate HMAC signature
            var signature = GenerateHmac(data, _configuration.SecretKey);

            // Combine data and signature into a token
            var token = $"{data}|{signature}";

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(token)); // Encode token to Base64
        }

        public bool ValidateEmailConfirmationToken(string token, string userEmail)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userEmail))
                return false;

            try
            {
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = decodedToken.Split('|');

                if (parts.Length != 3)
                    return false;

                var emailInToken = parts[0];
                var expirationTime = parts[1];
                var signature = parts[2];

                // Check if the email matches
                if (!emailInToken.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                    return false;

                // Check token expiration
                if (DateTime.TryParse(expirationTime, out var expirationDate))
                {
                    if (DateTime.UtcNow > expirationDate)
                        return false; // Token expired
                }
                else
                {
                    return false; // Invalid expiration time
                }

                // Validate signature
                var data = $"{emailInToken}|{expirationTime}";
                var expectedSignature = GenerateHmac(data, _configuration.SecretKey);

                return signature == expectedSignature;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateHmac(string data, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            using var hmac = new HMACSHA256(keyBytes);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}