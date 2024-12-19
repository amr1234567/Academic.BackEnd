namespace Academic.Services.Models.Inputs
{
    public class SolveTaskModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }

        public List<UserQuestionAnswerModel> Answers { get; set; }
    }
}