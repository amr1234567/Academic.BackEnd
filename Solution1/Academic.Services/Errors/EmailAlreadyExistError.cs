namespace Academic.Services.Errors
{
    public class EmailAlreadyExistError : Error
    {
        public EmailAlreadyExistError(string email) : base($"This Email '{email}' has been Found")
        {
        }
    }
}
