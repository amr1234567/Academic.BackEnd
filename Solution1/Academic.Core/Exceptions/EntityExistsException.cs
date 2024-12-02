using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Exceptions
{
    public class EntityExistsException : BaseException
    {
        public EntityExistsException(string typeName, object identity)
            : base($"Entity of type {typeName} already exist with marker : '{identity}'")
        { }
        public EntityExistsException(Type typeName, object identity)
           : base($"Entity of type {typeName.Name} already exist with marker : '{identity}'")
        { }
    }
}
