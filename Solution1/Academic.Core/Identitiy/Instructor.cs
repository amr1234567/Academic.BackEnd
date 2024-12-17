using Academic.Core.Base;
using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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

        [AllowNull]
        public string? ConfirmationToken { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string JobType { get; set; }

        public bool IsActive { get; set; }  

        public bool PasswordIsSet { get; set; }

        // Navigation Property
        public List<EducationalPath>? Paths { get; set; }

    }
}
