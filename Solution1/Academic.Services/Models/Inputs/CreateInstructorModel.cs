using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreateInstructorModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}