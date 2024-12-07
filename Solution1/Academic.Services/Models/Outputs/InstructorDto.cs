using Academic.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Outputs
{
    public class InstructorDto
    {
        public string Title { get; set; }

        public string JobType { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public ApplicationRole Role { get; set; }
    }
}