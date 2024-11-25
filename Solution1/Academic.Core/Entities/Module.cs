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
    public class Module: BaseEntity
    {
        [Required] 
        [MaxLength(150)]
        public string Title { get; set; }
        [Range(1, 5)]
        public double Difficulty { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, 50)]
        public int NumOfSections { get; set; }

        public bool IsNew => DateTime.UtcNow - CreatedAt < TimeSpan.FromDays(7);

        public TimeSpan ExpectedTimeToComplete { get; set; }

        public DateTime CreatedAt { get; set; }

        // 1(Path) - M(Module)
        [Required]
        public int PathId { get; set; }
        [ForeignKey(nameof(PathId))]
        public EducationalPath Path { get; set; }


        public ICollection<ModuleSection> Sections { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
