using Academic.Core.ComplexObjects;
using Academic.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class SignUpModel
    {
        [MaxLength(50)]
        [Required]
        public string Country { get; set; }

        [Required]
        [AllowedValues(typeof(EducationalLevel))]
        public string EducationalLevel { get; set; }
        [Required]
        [AllowedValues(typeof(EducationalClass))]
        public string EducationalClass { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [AllowNull]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}