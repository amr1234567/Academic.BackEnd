using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class UpdatePathTaskModel
    {
        [AllowNull]
        [MinLength(10)]
        public string? Title { get; set; }
        [AllowNull]
        [Range(1, 100)]
        public double? MinPercentagesToCertify { get; set; }
        [AllowNull]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public List<UpdatingQuestionModel>? Questions { get; set; }
    }
}