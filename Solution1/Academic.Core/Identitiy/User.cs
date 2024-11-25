using Academic.Core.Base;
using Academic.Core.ComplexObjects;
using Academic.Core.Entities;
using Academic.Core.Entities.ManyToManyEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Identitiy
{
    public class User : IdentityUser
    {

        /* perosnal info */
        [MaxLength(50)]
        [Required]
        public string Country { get; set; }

        [Required]
        public EducationStatus Education { get; set; }

        [Range(0, int.MaxValue)]
        public double Points { get; set; }

        // M - M
        public List<EducationalPath>? Paths { get; set; }

        public List<PathTask>? PathTasks { get; set; }

        // M - M
        public List<Module>? Modules { get; set; }

        public List<ModuleSection>? ModuleSections { get; set; }
    }
}
