using Academic.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Base
{
    public class IdentityUser : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [AllowNull]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        
        [AllowNull]
        public string? Salt { get; set; }

        [AllowNull]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public ApplicationRole Role { get; set; }

        [AllowNull]
        public string? RefreshToken { get; set; }

        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? RefreshTokenExpiredAt { get; set; }

        public bool IsUserLoggedIn => !string.IsNullOrWhiteSpace(RefreshToken) && RefreshTokenExpiredAt > DateTime.UtcNow;
    }
}
