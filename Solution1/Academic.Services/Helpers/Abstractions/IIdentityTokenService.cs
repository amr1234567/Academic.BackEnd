namespace Academic.Services.Helpers.Abstractions
{
    public interface IIdentityTokenService
    {
        string GenerateEmailConfirmationToken(string userEmail);
        bool ValidateEmailConfirmationToken(string token, string userEmail);
    }
}