using System.ComponentModel.DataAnnotations.Schema;

namespace Academic.Services.Models.Outputs
{
    public class PathDto
    {
        [NotMapped]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string IntroductionBody { get; set; }

        public double Difficulty { get; set; }

        public int NumOfModules { get; set; }

        public DateTime CreatedAt { get; set; }

        public int InstructorId { get; set; }

        public string InstructorName { get; set; } 

        public int? PathTaskId { get; set; }
        public string? PathTaskName { get; set; }

        public List<ModuleDto> Modules { get; set; } = [];
    }
}