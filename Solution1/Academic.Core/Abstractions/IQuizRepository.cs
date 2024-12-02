using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IQuizRepository
    {
        Task<int> GenerateNewQuizToSection(Quiz quiz);
        Task<int> UpdateQuizToSection(Quiz quiz);
        Task<Quiz> EmptyQuizFromQuestions(int quizId);
        Task<Quiz?> GetQuizById(int quizId);
        Task<Quiz?> GetQuizBySectionId(int sectionId);
        Task<int> AddQuestionsToSectionQuizBySectionId(int sectionId, params int[] questionId);
        Task<int> AddQuestionsToSectionQuizBySectionId(int sectionId, params MultiChoiceQuestion[] question);
        Task<int> AddQuestionsToSectionQuizByQuizId(int quizId, params int[] questionId);
        Task<int> AddQuestionsToSectionQuizByQuizId(int quizId, params MultiChoiceQuestion[] question);

        Task<int> RemoveQuestionsFromSectionQuizBySectionId(int sectionId, params int[] questionId);
        Task<int> RemoveQuestionsFromSectionQuizByQuizId(int quizId, params int[] questionId);
    }
}
