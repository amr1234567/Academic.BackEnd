namespace Academic.Services.Models.Outputs
{
    public class PathDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string IntroductionBody { get; set; }

        public double Difficulty { get; set; }

        public int NumOfModules { get; set; }

        public DateTime CreatedAt { get; set; }

        public string InstructorName { get; set; } 

        public string PathTaskName { get; set; } 

        public List<string> ModuleTitles { get; set; }
    }
}