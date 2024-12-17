using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Helpers
{
    public class TokenHelperConfigurations
    {
        public string SecretKey { get; set; }
        public int TokenExpirationMinutes { get; set; }
    }

}
