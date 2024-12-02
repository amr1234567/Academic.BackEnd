
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

        public async Task<int> GenerateNewQuestion(MultiChoiceQuestion question)
        {
            ArgumentNullException.ThrowIfNull(question, nameof(question));
            await context.MultiChoiceQuestions.AddAsync(question);
            return question.Id;
        }

        public async Task<MultiChoiceQuestion?> GetQuestion(int questionId)
        {
            var question = await context.MultiChoiceQuestions.AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Id == questionId);
            return question;
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
