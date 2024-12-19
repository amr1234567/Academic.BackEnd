using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreatingPathTaskModel
    {
        public int PathId { get; set; }
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        [Range(1, 100)]
        public double MinPercentagesToCertify { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public List<CreatingQuestionModel> Questions { get; set; }
    }
}