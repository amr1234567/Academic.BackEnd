using FluentResults;

namespace Academic.Core.Errors
{
    public class BadRequestError : Error
    {
        private BadRequestError(Type type) : base($"[BAD REQUEST] from type '{type.Name}'") { }
        private BadRequestError(string message) : base(message) { }
        public static BadRequestError Exists(Type type) => new(type);
        public static BadRequestError Exists<T>() => new(typeof(T));
        public static BadRequestError Exists(string message) => new(message);
    }
}