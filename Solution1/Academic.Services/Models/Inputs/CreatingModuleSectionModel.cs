using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class CreatingModuleSectionModel
    {

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [Required]
        public string Body { get; set; }

        [Required]
        public int ModuleId { get; set; }
    }
}