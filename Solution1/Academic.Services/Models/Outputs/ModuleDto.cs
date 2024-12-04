using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Outputs
{
    public class ModuleDto
    {
        public string Title { get; set; }  

        public double Difficulty { get; set; } 

        public string Description { get; set; }  

        public int NumOfSections { get; set; }  

        public bool IsNew { get; set; }  

        public TimeSpan ExpectedTimeToComplete { get; set; } 

        public DateTime CreatedAt { get; set; } 

        public string PathName { get; set; }
    }
}