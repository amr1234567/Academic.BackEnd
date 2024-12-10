using Academic.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IQuizRepository
    {
        Task<Result> GenerateNewQuizToSection(Quiz quiz);
        Task<Result> UpdateQuizToSection(Quiz quiz);
        Task<Quiz> EmptyQuizFromQuestions(int quizId);
        Task<Quiz?> GetQuizById(int quizId);
        Task<Quiz?> GetQuizWithQuestionsById(int quizId);
        Task<Quiz?> GetQuizBySectionId(int sectionId);
        Task<Quiz?> GetQuizWithQuestionsBySectionId(int sectionId);
        Task<Result> AddQuestionsToSectionQuizBySectionId(int sectionId, params int[] questionId);
        Task<Result> AddQuestionsToSectionQuizBySectionId(int sectionId, params MultiChoiceQuestion[] question);
        Task<Result> AddQuestionsToSectionQuizByQuizId(int quizId, params int[] questionId);
        Task<Result> AddQuestionsToSectionQuizByQuizId(int quizId, params MultiChoiceQuestion[] question);

        Task<Result> RemoveQuestionsFromSectionQuizBySectionId(int sectionId, params int[] questionId);
        Task<Result> RemoveQuestionsFromSectionQuizByQuizId(int quizId, params int[] questionId);
    }
}
