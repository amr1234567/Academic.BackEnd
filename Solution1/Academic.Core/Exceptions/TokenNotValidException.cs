
namespace Academic.Core.Exceptions
{
    [Serializable]
    public class TokenNotValidException : BaseException
    {
        public TokenNotValidException(string? message) : base(message)
        {
        }

    }
}