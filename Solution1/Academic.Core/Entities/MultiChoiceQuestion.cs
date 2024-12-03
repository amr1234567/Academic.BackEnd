using Academic.Core.Base;
using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities
{
    public class MultiChoiceQuestion : BaseEntity
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

        [Range(1,20)]
        public double Points { get; set; }


        [Required]
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
    }
}
