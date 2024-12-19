using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities.ManyToManyEntities
{
    public class PathTaskUsers
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PathTaskId { get; set; }
        public PathTask PathTask { get; set; }

        [Required]
        public bool HasCertification { get; set; }
        [Required]
        [Range(0, 200)]
        public double Score { get; set; }

        public List<UserQuestionAnswer> Answers { get; set; }
    }

}
