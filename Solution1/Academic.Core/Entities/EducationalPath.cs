using Academic.Core.Base;
using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities
{
    [Table("Paths")]
    public class EducationalPath : BaseEntity
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
        public double Difficulty { get; set; }

        [Range(1, int.MaxValue)]
        public int NumOfModules { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 1(Path) - M(Module)
        public List<Module>? Modules { get; set; }

        [Required]
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }

        [Required]
        public int PathTaskId { get; set; }
        [ForeignKey(nameof(PathTaskId))]
        public PathTask PathTask { get; set; }

        // M - M
        public List<User> Users { get; set; }
    }
}
