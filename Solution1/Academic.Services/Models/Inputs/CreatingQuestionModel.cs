using Academic.Core.Entities;
using Academic.Core.Identitiy;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreatingQuestionModel
    {
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        /* Options */
        [Required]
        public string ChoiceA { get; set; }
        [Required]
        public string ChoiceB { get; set; }
        [Required]
        public string ChoiceC { get; set; }
        [Required]
        public string ChoiceD { get; set; }

        [Required]
        [AllowedValues('A', 'B', 'C', 'D')]
        public char Answer { get; set; }

        [Range(1, 20)]
        public double Points { get; set; }


        [Required]
        public int InstructorId { get; set; }
       
    }
}