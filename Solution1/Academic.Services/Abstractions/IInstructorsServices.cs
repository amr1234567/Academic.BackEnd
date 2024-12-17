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
        Task<Result> CreateNewInstructor(ConfirmAccountFromInstructorModel model);

        Task<Result> CreatePath(CreatingPathModel model);
        Task<Result> DeletePath(int pathId);
        Task<Result> UpdatePathDetails(int id, UpdatePathModel model);

        Task<Result> CreateModule(CreatingModuleModel model);
        Task<Result> DeleteModule(int moduleId);
        Task<Result> UpdateModuleDetails(int id, UpdateModuleModel model);

        Task<Result> GenerateSection(CreatingModuleSectionModel model);
        Task<Result> UpdateSectionDetails(int id, UpdatingModuleSectionModel model);
        Task<Result> AddQuestionToSection(int id, CreatingQuestionModel model);
        Task<Result> AddExistingQuestionToSection(int id, int questionId);
        Task<Result> DeleteQuestionFromSection(int id, int questionId);
        Task<Result> DeleteSection(int sectionId);

        Task<Result> GenerateTask(CreatingPathTaskModel model);
        Task<Result> DeleteTask(int taskId);
        Task<Result> UpdatePathTask(int id, UpdatePathTaskModel model);
        Task<Result> AddQuestionToTask(int taskId, CreatingQuestionModel model);
        Task<Result> AddExistingQuestionToTask(int taskId, int questionId);
        Task<Result> DeleteQuestionFromTask(int taskId, int questionId);

        Task<Result> UpdateInstructorDetails(UpdateInstructorModel model);
    }
}
