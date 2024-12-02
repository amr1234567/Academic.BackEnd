﻿using Academic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IQuestionRepository
    {
        Task<int> GenerateNewQuestion(MultiChoiceQuestion question);
        Task<int> UpdateQuestion(MultiChoiceQuestion question);
        Task<int> DeleteQuestion(int questionId);
        Task<MultiChoiceQuestion?> GetQuestion(int questionId);
        Task<List<MultiChoiceQuestion>> GetQuestionsForInstructor(int instructorId, int page = 1, int size = 30);
        Task<List<MultiChoiceQuestion>> GetQuestionsForInstructorBySearch(int instructorId, string searchText = "", int page = 1, int size = 30);
    }
}
