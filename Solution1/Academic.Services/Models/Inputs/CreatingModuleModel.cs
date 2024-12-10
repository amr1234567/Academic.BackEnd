using Academic.Core.Entities;
using Academic.Core.Identitiy;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreatingModuleModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Range(1, 5)]
        public double Difficulty { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }


        // 1(Path) - M(Module)
        [Required]
        public int PathId { get; set; }


    }
}