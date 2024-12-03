using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Models.Outputs
{
    public class TokenModel
    {
        public string Token { get; internal set; }
        public string RefreshToken { get; internal set; }
        public DateTime ExpireDate { get; internal set; }
    }
}
