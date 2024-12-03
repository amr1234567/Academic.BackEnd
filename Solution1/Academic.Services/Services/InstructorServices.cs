using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class InstructorServices : IInstructorsServices
    {
        public Task<ModuleSectionDto> AddExistingQuestionToSection(int id, int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PathTaskModel> AddExistingQuestionToTask(int taskId, int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleSectionDto> AddQuestionToSection(int id, CreatingQuestionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PathTaskModel> AddQuestionToTask(int taskId, CreatingQuestionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateModel(CreatingModuleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePath(CreatingPathModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleSectionDto> DeleteFromSectionQuestion(int id, int questionId)
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

        public Task<PathTaskModel> DeleteQuestionFromTask(int taskId, int questionId)
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

        public Task<InstructorDto> UpdateInstructorDetails(UpdateInstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleDto> UpdateModuleDetails(int id, UpdateModuleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PathDto> UpdatePathDetails(int id, UpdatePathModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PathTaskModel> UpdatePathTask(int id, UpdatePathTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleSectionDto> UpdateSectionDetails(int id, UpdatingModuleSectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
