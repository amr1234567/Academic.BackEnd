using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Errors
{
    public class EntityNotFoundError : Error
    {

        private EntityNotFoundError(Type typeName, object identifier) 
            : base($"entity from type '{typeName.Name}' with identifier '{identifier}' [NOT FOUND]")
        {
        }
        private EntityNotFoundError(Type typeName)
            : base($"entity from type '{typeName.Name}' [NOT FOUND]")
        {
        }
        private EntityNotFoundError(string typeName, object identifier)
            : base($"entity from type '{typeName}' with identifier '{identifier}' [NOT FOUND]")
        {
        }
        private EntityNotFoundError(string typeName)
            : base($"entity from type '{typeName}' [NOT FOUND]")
        {
        }
        public static EntityNotFoundError Exist(Type typeName, object identifier) => new(typeName, identifier);
        public static EntityNotFoundError Exist(Type typeName) => new(typeName);
        public static EntityNotFoundError Exist(string typeName, object identifier) => new(typeName, identifier);
        public static EntityNotFoundError Exist(string typeName) => new(typeName);
    }
}
