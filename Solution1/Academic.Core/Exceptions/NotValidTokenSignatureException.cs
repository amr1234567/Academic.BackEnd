
namespace Academic.Core.Exceptions
{
    [Serializable]
    public class NotValidTokenSignatureException : BaseException
    {

        public NotValidTokenSignatureException(string? message) : base(message)
        {
        }

    }
}