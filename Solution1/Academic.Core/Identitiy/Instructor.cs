using Academic.Core.Base;
using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Identitiy
{
    public class Instructor : IdentityUser
    {
        /* Credentials */
        [Required]
        public string HashedPassword { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string JobType { get; set; }

        // Added new 
        //public bool IsBlocked { get; set; } = false;
        public bool IsActive { get; set; }  

        // Navigation Property
        public List<EducationalPath>? Paths { get; set; }

    }
}
