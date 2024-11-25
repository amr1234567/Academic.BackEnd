using Academic.Core.Base;
using Academic.Core.Entities.ManyToManyEntities;
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
    public class PathTask : BaseEntity
    {

        [Range(1, 200)]
        public double TotalPoints { get; set; }
        [Range(1, 140)]
        public double MinPointsToCertify { get; set; }
        [Required] 
        [MaxLength(200)]
        public string Description { get; set; }

        // M - M
        public List<MultiChoiceQuestion> Questions { get; set; }

        public List<User> Users { get; set; }

        [Required]
        public int PathId { get; set; }
        [ForeignKey(nameof(PathId))]
        public EducationalPath Path { get; set; }

    }
}
