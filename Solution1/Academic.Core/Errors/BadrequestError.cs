using FluentResults;

namespace Academic.Core.Errors
{
    public class BadRequestError : Error
    {
        private BadRequestError(Type type) : base($"[BAD REQUEST] from type '{type.Name}'") { }

        public static BadRequestError Exists(Type type) => new(type);
    }
}