using Academic.Core.Entities;
using Academic.Core.Identitiy;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreatingPathModel
    {
        /* Path info */
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Html)]
        public string? IntroductionBody { get; set; }

        /* indication */
        [Range(1, 5)]
        public double Difficulty { get; set; } = 0;

        [Required]
        public int InstructorId { get; set; }

    }
}