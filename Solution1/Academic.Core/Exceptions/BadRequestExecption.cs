
namespace Academic.Services.Services
{
    [Serializable]
    internal class BadRequestExecption : Exception
    {
        public BadRequestExecption()
        {
        }

        public BadRequestExecption(string? message) : base(message)
        {
        }

        public BadRequestExecption(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}