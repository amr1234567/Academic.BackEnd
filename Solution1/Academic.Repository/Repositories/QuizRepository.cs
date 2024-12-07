using Academic.Core.Abstractions;
using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Academic.Repository.Repositories
{
    public class QuizRepository
        (ApplicationDbContext context, IQuestionRepository questionRepository,IModuleSectionsRepository moduleSectionsRepository) 
        : IQuizRepository
    {
        public async Task<int> AddQuestionsToSectionQuizByQuizId(int quizId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
                .Include(quiz => quiz.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId)
                 ?? throw new EntityNotFoundException(typeof(Quiz), quizId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId)
                    ?? throw new EntityNotFoundException(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Add(question.Value);
            }
            quiz.Questions = quiz.Questions.Distinct().ToList();
            return quiz.Questions.Count;
        }

        public async Task<int> AddQuestionsToSectionQuizByQuizId(int quizId, params MultiChoiceQuestion[] questions)
        {
            var quiz = await context.Quizzes
                .Include(quiz => quiz.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId)
                 ?? throw new EntityNotFoundException(typeof(Quiz), quizId);
            foreach (var question in questions)
            {
                quiz.Questions.Add(question);
            }
            quiz.Questions = quiz.Questions.Distinct().ToList();
            return quiz.Questions.Count;
        }

        public async Task<int> AddQuestionsToSectionQuizBySectionId(int sectionId, params int[] questionIds)
        {
            var section = await moduleSectionsRepository.GetModuleSectionById(sectionId)
                ?? throw new EntityNotFoundException(typeof(ModuleSection), sectionId);
            return await AddQuestionsToSectionQuizByQuizId(section.QuizId, questionIds);
        }

        public async Task<int> AddQuestionsToSectionQuizBySectionId(int sectionId, params MultiChoiceQuestion[] questions)
        {
            var section = await moduleSectionsRepository.GetModuleSectionById(sectionId)
                ?? throw new EntityNotFoundException(typeof(ModuleSection), sectionId);
            return await AddQuestionsToSectionQuizByQuizId(section.QuizId, questions);
        }

        public async Task<Quiz> EmptyQuizFromQuestions(int quizId)
        {
            var quiz = await context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == quizId)
                 ?? throw new EntityNotFoundException(typeof(Quiz), quizId);
            quiz.Questions.RemoveAll(q => true);
            return quiz;
        }

        public async Task<int> GenerateNewQuizToSection(Quiz quiz)
        {
            ArgumentNullException.ThrowIfNull(quiz, nameof(quiz));
            await context.Quizzes.AddAsync(quiz);
            return quiz.Id;
        }

        public async Task<Quiz?> GetQuizById(int quizId)
        {
            return await context.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<Quiz?> GetQuizBySectionId(int sectionId)
        {
            return await context.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.SectionId == sectionId);
        }

        public async Task<int> RemoveQuestionsFromSectionQuizByQuizId(int quizId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
               .Include(quiz => quiz.Questions)
               .FirstOrDefaultAsync(q => q.Id == quizId)
                ?? throw new EntityNotFoundException(typeof(Quiz), quizId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId)
                     ?? throw new EntityNotFoundException(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Remove(question.Value);
            }
            return quiz.Questions.Count;
        }

        public async Task<int> RemoveQuestionsFromSectionQuizBySectionId(int sectionId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
              .Include(quiz => quiz.Questions)
              .FirstOrDefaultAsync(q => q.SectionId == sectionId)
               ?? throw new EntityNotFoundException(typeof(Quiz), sectionId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId)
                     ?? throw new EntityNotFoundException(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Remove(question.Value);
            }
            return quiz.Questions.Count;
        }

        public async Task<int> UpdateQuizToSection(Quiz quiz)
        {
            return await context.Quizzes.Where(q => q.Id == quiz.Id)
                .ExecuteUpdateAsync(ques =>
                ques.SetProperty(q => q.Title, quiz.Title)
                );
        }
    }
}
