using Academic.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
        public ApplicationRole Role { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredAt { get; set; }

        public bool IsUserLoggedIn => !string.IsNullOrWhiteSpace(RefreshToken) && RefreshTokenExpiredAt > DateTime.UtcNow;
    }
}
