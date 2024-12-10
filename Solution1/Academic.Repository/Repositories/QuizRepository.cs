using Academic.Core.Abstractions;
using Academic.Core.Entities;
using Academic.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Academic.Repository.Repositories
{
    public class QuizRepository
        (ApplicationDbContext context, IQuestionRepository questionRepository,
        IModuleSectionsRepository moduleSectionsRepository) 
        : IQuizRepository
    {
        public async Task<Result> AddQuestionsToSectionQuizByQuizId(int quizId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
                .Include(quiz => quiz.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);
            if(quiz == null)
                return EntityNotFoundError.Exists(typeof(Quiz), quizId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId);
                if (question == null)
                    return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Add(question.Value);
            }
            quiz.Questions = quiz.Questions.Distinct().ToList();
            return Result.Ok();
        }

        public async Task<Result> AddQuestionsToSectionQuizByQuizId(int quizId, params MultiChoiceQuestion[] questions)
        {
            var quiz = await context.Quizzes
                .Include(quiz => quiz.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz == null)
                return EntityNotFoundError.Exists(typeof(Quiz), quizId);
            foreach (var question in questions)
            {
                quiz.Questions.Add(question);
            }
            quiz.Questions = quiz.Questions.Distinct().ToList();
            return Result.Ok();
        }

        public async Task<Result> AddQuestionsToSectionQuizBySectionId(int sectionId, params int[] questionIds)
        {
            var section = await moduleSectionsRepository.GetModuleSectionById(sectionId);
            if (section == null)
                return EntityNotFoundError.Exists(typeof(ModuleSection), sectionId);
            return await AddQuestionsToSectionQuizByQuizId(section.QuizId, questionIds);
        }

        public async Task<Result> AddQuestionsToSectionQuizBySectionId(int sectionId, params MultiChoiceQuestion[] questions)
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

        public async Task<Result> GenerateNewQuizToSection(Quiz quiz)
        {
            if (quiz is null)
                return BadRequestError.Exists(typeof(Quiz));
            await context.Quizzes.AddAsync(quiz);
            return Result.Ok();
        }

        public async Task<Quiz?> GetQuizById(int quizId)
        {
            return await context.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<Quiz?> GetQuizBySectionId(int sectionId)
        {
            return await context.Quizzes.AsNoTracking().FirstOrDefaultAsync(q => q.SectionId == sectionId);
        }

        public async Task<Quiz?> GetQuizWithQuestionsById(int quizId)
        {
            var quiz = await context.Quizzes.Include(q=>q.Questions)
                .AsNoTracking().FirstOrDefaultAsync(q => q.Id == quizId);
            return quiz;
        }

        public async Task<Quiz?> GetQuizWithQuestionsBySectionId(int sectionId)
        {
            var quiz = await context.Quizzes.Include(q => q.Questions)
                .AsNoTracking().FirstOrDefaultAsync(q => q.SectionId == sectionId);
            return quiz;
        }

        public async Task<Result> RemoveQuestionsFromSectionQuizByQuizId(int quizId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
               .Include(quiz => quiz.Questions)
               .FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz is null)
                return EntityNotFoundError.Exists(typeof(Quiz), quizId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId);
                if (question is null)
                    return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Remove(question.Value);
            }
            return Result.Ok();
        }

        public async Task<Result> RemoveQuestionsFromSectionQuizBySectionId(int sectionId, params int[] questionIds)
        {
            var quiz = await context.Quizzes
              .Include(quiz => quiz.Questions)
              .FirstOrDefaultAsync(q => q.SectionId == sectionId);
            if (quiz is null)
                return EntityNotFoundError.Exists(typeof(Quiz), sectionId);
            foreach (var questionId in questionIds)
            {
                var question = await questionRepository.GetQuestion(questionId);
                if (question is null)
                    return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);
                quiz.Questions.Remove(question.Value);
            }
            return Result.Ok();
        }

        public async Task<Result> UpdateQuizToSection(Quiz quiz)
        {
            if (quiz is null)
                return BadRequestError.Exists(typeof(Quiz));
            await context.Quizzes.Where(q => q.Id == quiz.Id)
                .ExecuteUpdateAsync(ques =>
                ques.SetProperty(q => q.Title, quiz.Title)
                );
            return Result.Ok();
        }
    }
}
