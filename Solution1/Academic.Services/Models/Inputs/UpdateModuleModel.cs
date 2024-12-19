using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class UpdateModuleModel
    {
        [AllowNull]
        [MaxLength(150)]
        public string? Title { get; set; }
        [Range(1, 5)]
        [AllowNull]
        public double? Difficulty { get; set; }
        [MaxLength(500)]
        [AllowNull]
        public string? Description { get; set; }

    }
}