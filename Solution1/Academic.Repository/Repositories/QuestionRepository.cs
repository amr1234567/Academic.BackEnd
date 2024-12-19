
using Academic.Core.Errors;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
    {
        public async Task<int> DeleteQuestion(int questionId)
        {
            return await context.MultiChoiceQuestions.Where(q => q.Id == questionId).ExecuteDeleteAsync();

        }

        public async Task<Result> GenerateNewQuestion(MultiChoiceQuestion question)
        {
            ArgumentNullException.ThrowIfNull(question, nameof(question));
            await context.MultiChoiceQuestions.AddAsync(question);
            return Result.Ok();
        }

        public async Task<Result<MultiChoiceQuestion>> GetAllAnswerForQuestion(int questionId, int page = 1, int size = 30)
        {
            var question = await context.MultiChoiceQuestions.AsNoTracking()
                .Include(q=>q.UsersAnswers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
            if (question == null)
                return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);
            return Result.Ok(question);
        }

        public async Task<Result<MultiChoiceQuestion>> GetQuestion(int questionId)
        {
            var question = await context.MultiChoiceQuestions.AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Id == questionId);
            return question is null ?
                Result.Fail(EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId)) :
                question.ToResult();
        }

        public async Task<List<MultiChoiceQuestion>> GetQuestionsForInstructor(int instructorId, int page = 1, int size = 10)
        {
            var questions = await context.MultiChoiceQuestions
                    .AsNoTracking() 
                    .Where(q => q.InstructorId == instructorId)
                    .Skip((page - 1) * size).Take(size).ToListAsync();
            return questions;
        } 

        public async Task<List<MultiChoiceQuestion>> GetQuestionsForInstructorBySearch
            (int instructorId, string searchText = "", int page = 1, int size = 10)
        {
            var questions = await context.MultiChoiceQuestions
                    .AsNoTracking() 
                    .Where(q => q.InstructorId == instructorId)
                    .Where(q => q.Content.Contains(searchText))
                    .Skip((page - 1) * size).Take(size).ToListAsync();

            return questions;
        }

        public async Task<Result<List<MultiChoiceQuestion>>> GetQuestionsInSectionForUser(int quizId, int userId)
        {
            var questions = await context.MultiChoiceQuestions.AsNoTracking()
                .Where(q => q.UsersAnswers.Any(a => a.UserId == userId))
                .Where(q => q.Quizs.Any(quiz => quiz.Id == quizId))
                .ToListAsync();
            return Result.Ok(questions);
        }

        public async Task<Result<double>> SolveQuestion(UserQuestionAnswer model)
        {
            if (model == null)
                return BadRequestError.Exists(typeof(UserQuestionAnswer));
            var questionResult = await GetQuestion(model.QuestionId);
            if (questionResult.IsFailed)
                return Result.Fail(questionResult.Errors);
            var newModel = model;
            newModel.IsCorrect = questionResult.Value.Answer == model.UserChoice;
            await context.userQuestionAnswers.AddAsync(newModel);
            return Result.Ok(newModel.IsCorrect ? questionResult.Value.Points : 0);
        }

        public async Task<int> UpdateQuestion(MultiChoiceQuestion question)
        {
            return await context.MultiChoiceQuestions.Where(q => q.Id == question.Id)
                .ExecuteUpdateAsync(ques =>
                ques.SetProperty(q => q.Answer, question.Answer)
                    .SetProperty(q => q.Answer, question.Answer)
                    .SetProperty(q => q.Content, question.Content)
                    .SetProperty(q => q.ChoiceA, question.ChoiceA)
                    .SetProperty(q => q.ChoiceB, question.ChoiceB)
                    .SetProperty(q => q.ChoiceC, question.ChoiceC)
                    .SetProperty(q => q.ChoiceD, question.ChoiceD)
                    .SetProperty(q => q.Points, question.Points)
                );
        }

    }
}
