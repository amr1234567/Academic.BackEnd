using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities.ManyToManyEntities
{
    public class ModuleUsers
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

        [Range(0, 100)]
        [Required]
        public double ProgressPresented { get; set; }
    }
}
