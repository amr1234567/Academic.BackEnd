namespace Academic.Core.Exceptions
{
    [Serializable]
    internal class SecurityTokenExpiredException : Exception
    {
        public SecurityTokenExpiredException()
        {
        }

        public SecurityTokenExpiredException(string? message) : base(message)
        {
        }

        public SecurityTokenExpiredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}