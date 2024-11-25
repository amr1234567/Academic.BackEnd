using Academic.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities
{
    public class Quiz : BaseEntity
    {
        [Required] 
        [MaxLength(200)]
        public string Title { get; set; }

        // M - M
        public List<MultiChoiceQuestion> Questions { get; set; }

        [Required]
        public int SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public ModuleSection Section { get; set; }
    }
}
