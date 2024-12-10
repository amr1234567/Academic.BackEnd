namespace Academic.Services.Errors
{
    public class SecurityTokenExpiredError : Error
    {

        public SecurityTokenExpiredError(string message) : base(message)
        {
        }
        public SecurityTokenExpiredError() : base("Token is Expired") { }
    }
}
