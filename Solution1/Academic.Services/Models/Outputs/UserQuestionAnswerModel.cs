namespace Academic.Services.Models.Outputs
{
    public class UserQuestionAnswerModel
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public char UserChoice { get; set; }
    }
}