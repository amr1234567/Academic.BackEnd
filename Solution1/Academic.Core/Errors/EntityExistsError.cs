using FluentResults;

namespace Academic.Core.Errors
{
    public class EntityExistsError : Error
    {
        private EntityExistsError(Type type) : base($"[BAD REQUEST] Entity Found Already '{type.Name}'") { }
        private EntityExistsError(string message) : base(message) { }
        public static EntityExistsError Exists(Type type) => new(type);
        public static EntityExistsError Exists<T>() => new(typeof(T));
        public static EntityExistsError Exists(string message) => new(message);
    }
}