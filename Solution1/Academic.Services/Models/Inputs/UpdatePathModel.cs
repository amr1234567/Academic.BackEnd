using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class UpdatePathModel
    {
        [AllowNull]
        [MaxLength(200)]
        public string? Title { get; set; }
        [MaxLength(500)]
        [AllowNull]
        public string? Description { get; set; }


        [AllowNull]
        [DataType(DataType.Html)]
        public string? IntroductionBody { get; set; }

        /* indication */
        [AllowNull]
        [Range(1, 5)]
        public double? Difficulty { get; set; }
    }
}