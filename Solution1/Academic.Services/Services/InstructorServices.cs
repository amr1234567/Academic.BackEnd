using Academic.Core.Entities;
using Academic.Core.Errors;
using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class InstructorServices
        (IQuestionRepository questionRepository, IModuleSectionsRepository sectionsRepository,
        IPathTasksRepository pathTasksRepository, IModuleRepository moduleRepository,
        IPathRepository pathRepository, IUnitOfWork unitOfWork, IQuizRepository quizRepository,IMapper mapper)
        : IInstructorsServices
    {
        public async Task<int> AddExistingQuestionToSection(int quizId, int questionId)
        {
            var sectionQuiz = await quizRepository.GetQuizById(quizId);
            if (sectionQuiz == null )
            {
                throw new EntityNotFoundException(typeof(Quiz), quizId);
            }
            var questionResult = await questionRepository.GetQuestion(questionId);
            if (questionResult.IsFailed)
            {
                throw new EntityNotFoundException(typeof(MultiChoiceQuestion), questionId);

            }
            await quizRepository.AddQuestionsToSectionQuizByQuizId(quizId, questionResult.Value);
            await unitOfWork.SaveChangesAsync();
            return quizId;
        }

        public async Task<int> AddExistingQuestionToTask(int taskId, int questionId)
        {
            var pathTask = await pathTasksRepository.GetTaskForPathById(taskId);
            if (pathTask == null)
            {
                throw new EntityNotFoundException(typeof(PathTask), taskId);
            }
            var questionResult = await questionRepository.GetQuestion(questionId);
            if (questionResult.IsFailed)
            {
                throw new EntityNotFoundException(typeof(MultiChoiceQuestion), questionId);

            }
            await pathTasksRepository.AddQuestionsToTask(taskId, questionResult.Value);
            await unitOfWork.SaveChangesAsync();
            return taskId;
        }

        public Task<int> AddQuestionToSection(int id, CreatingQuestionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddQuestionToTask(int taskId, CreatingQuestionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateModule(CreatingModuleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePath(CreatingPathModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteFromSectionQuestion(int id, int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteModule(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePath(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteQuestionFromTask(int taskId, int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSection(int sectionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GenerateSection(CreatingModuleSectionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> GenerateTask(CreatingPathTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateInstructorDetails(UpdateInstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateModuleDetails(int id, UpdateModuleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePathDetails(int id, UpdatePathModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePathTask(int id, UpdatePathTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSectionDetails(int id, UpdatingModuleSectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
