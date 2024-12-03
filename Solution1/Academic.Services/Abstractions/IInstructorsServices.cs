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
        Task<PathDto> UpdatePathDetails(int id, UpdatePathModel model);

        Task<int> CreateModel(CreatingModuleModel model);
        Task<int> DeleteModule(int pathId);
        Task<ModuleDto> UpdateModuleDetails(int id, UpdateModuleModel model);

        Task<int> GenerateSection(CreatingModuleSectionModel model);
        Task<ModuleSectionDto> UpdateSectionDetails(int id, UpdatingModuleSectionModel model);
        Task<ModuleSectionDto> AddQuestionToSection(int id, CreatingQuestionModel model);
        Task<ModuleSectionDto> AddExistingQuestionToSection(int id, int questionId);
        Task<ModuleSectionDto> DeleteFromSectionQuestion(int id, int questionId);
        Task<int> DeleteSection(int sectionId);

        Task<int> GenerateTask(CreatingPathTaskModel model);
        Task<int> DeleteTask(int taskId);
        Task<PathTaskModel> UpdatePathTask(int id, UpdatePathTaskModel model);
        Task<PathTaskModel> AddQuestionToTask(int taskId, CreatingQuestionModel model);
        Task<PathTaskModel> AddExistingQuestionToTask(int taskId, int questionId);
        Task<PathTaskModel> DeleteQuestionFromTask(int taskId, int questionId);

        Task<InstructorDto> UpdateInstructorDetails(UpdateInstructorModel model);
    }
}
