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
    public class ModuleSection : BaseEntity
    {

        [Required] 
        [MaxLength(150)]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [Required]
        public string Body { get; set; }

        // 1 - 1
        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; }

        [Required]
        public int ModuleId { get; set; }
        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; }

        public List<User> Users { get; set; }

    }
}
