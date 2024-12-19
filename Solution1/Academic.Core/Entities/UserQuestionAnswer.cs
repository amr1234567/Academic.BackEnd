using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Entities
{
    public class UserQuestionAnswer
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int? QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz? Quiz { get; set; }

        public int? PathTaskId { get; set; }
        [ForeignKey(nameof(PathTaskId))]
        public PathTask? PathTask { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public MultiChoiceQuestion Question { get; set; }

        [Required]
        public char UserChoice { get; set; }

        public bool IsCorrect { get; set; }
    }
}
