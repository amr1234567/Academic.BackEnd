using Academic.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Identitiy
{
    public class Admin : IdentityUser
    {
        [Required]
        public string HashPassword { get; set; }
    }
}
