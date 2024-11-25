using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities.ManyToManyEntities
{
    public class PathUsers
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PathId { get; set; }
        public EducationalPath Path { get; set; }


        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfCompletedModules { get; set; }
        [Required]
        public bool IsCompleted {  get; set; }
    }
}
