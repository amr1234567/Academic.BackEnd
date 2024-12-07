using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IInstructorsServices
    {
        Task<int> CreatePath(CreatingPathModel model);
        Task<int> DeletePath(int pathId);
        Task<int> UpdatePathDetails(int id, UpdatePathModel model);

        Task<int> CreateModule(CreatingModuleModel model);
        Task<int> DeleteModule(int pathId);
        Task<int> UpdateModuleDetails(int id, UpdateModuleModel model);

        Task<int> GenerateSection(CreatingModuleSectionModel model);
        Task<int> UpdateSectionDetails(int id, UpdatingModuleSectionModel model);
        Task<int> AddQuestionToSection(int id, CreatingQuestionModel model);
        Task<int> AddExistingQuestionToSection(int id, int questionId);
        Task<int> DeleteFromSectionQuestion(int id, int questionId);
        Task<int> DeleteSection(int sectionId);

        Task<int> GenerateTask(CreatingPathTaskModel model);
        Task<int> DeleteTask(int taskId);
        Task<int> UpdatePathTask(int id, UpdatePathTaskModel model);
        Task<int> AddQuestionToTask(int taskId, CreatingQuestionModel model);
        Task<int> AddExistingQuestionToTask(int taskId, int questionId);
        Task<int> DeleteQuestionFromTask(int taskId, int questionId);

        Task<int> UpdateInstructorDetails(UpdateInstructorModel model);
    }
}
