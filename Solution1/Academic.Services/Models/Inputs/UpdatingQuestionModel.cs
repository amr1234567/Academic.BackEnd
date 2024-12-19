using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academic.Services.Models.Inputs
{
    public class UpdatingQuestionModel
    {
        [AllowNull]
        public int? Id { get; set; }

        public bool WantDelete { get; set; }

        [AllowNull]
        [DataType(DataType.Html)]
        public string? Content { get; set; }

        /* Options */
        [AllowNull]
        public string? ChoiceA { get; set; }
        [AllowNull]
        public string? ChoiceB { get; set; }
        [AllowNull]
        public string? ChoiceC { get; set; }
        [AllowNull]
        public string? ChoiceD { get; set; }

        [AllowNull]
        [AllowedValues('A', 'B', 'C', 'D')]
        public char? Answer { get; set; }

        [Range(1, 20)]
        public double? Points { get; set; }


        [AllowNull]
        public int? InstructorId { get; set; }

        private UpdatingQuestionModel
            (int? id = null, string? content = null, string? choiceA = null, 
            string? choiceB = null, string? choiceC = null, string? choiceD = null,
            char? answer = null, double? points = null, int? instructorId = null)
        {
            Id = id;
            Content = content;
            ChoiceA = choiceA;
            ChoiceB = choiceB;
            ChoiceC = choiceC;
            ChoiceD = choiceD;
            Answer = answer;
            Points = points;
            InstructorId = instructorId;
        }
    
        public static UpdatingQuestionModel CreateUpdatingQuestion
            (int id, string content, string choiceA, string choiceB,
            string choiceC, string choiceD, char answer, double points)
        {
            var question =  new UpdatingQuestionModel(id, content, choiceA, choiceB,
                choiceC, choiceD, answer, points);
            question.WantDelete = false;
            return question;
        }

        public static UpdatingQuestionModel CreateNewQuestion
            (string content, string choiceA, string choiceB, string choiceC,
            string choiceD, char answer, double points, int instructorId)
        {
            var question = new UpdatingQuestionModel(null, content, choiceA, choiceB, choiceC,
                choiceD, answer, points, instructorId);
            question.WantDelete = false;
            return question;
        }

        public static UpdatingQuestionModel CreateNewDeleteQuestion
            (int id)
        {
            var question = new UpdatingQuestionModel(id);
            question.WantDelete = true;
            return question;
        }
    }
}