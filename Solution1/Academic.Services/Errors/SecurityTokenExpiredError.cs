namespace Academic.Services.Errors
{
    public class SecurityTokenExpiredError : Error
    {

        public SecurityTokenExpiredError(string message) : base(message)
        {
        }
        public SecurityTokenExpiredError() : base("Token is Expired") { }
    }

    public class UnauthorizedError : Error
    {
        private UnauthorizedError(string message): base(message) 
        {
            
        }

        public static UnauthorizedError Exists(string email)
        {
            return new($"unauthorized access tries by {email}");
        }
    }
}
