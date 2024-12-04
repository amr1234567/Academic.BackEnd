namespace Academic.Services.Models.Outputs
{
    public class ModuleSectionDto
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int QuizId { get; set; }

        public int ModuleId { get; set; }
    }
}