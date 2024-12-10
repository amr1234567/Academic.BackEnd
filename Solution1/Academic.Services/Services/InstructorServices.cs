using Academic.Core.Entities;
using Academic.Services.Models.Inputs;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class InstructorServices
        (IQuestionRepository questionRepository, IModuleSectionsRepository sectionsRepository,
        IPathTasksRepository pathTasksRepository, IModuleRepository moduleRepository,
        IPathRepository pathRepository, IUnitOfWork unitOfWork, IQuizRepository quizRepository,IMapper mapper)
        : IInstructorsServices
    {
        public async Task<Result> AddExistingQuestionToSection(int quizId, int questionId)
        {
            var sectionQuiz = await quizRepository.GetQuizById(quizId);
            if (sectionQuiz == null )
            {
                return EntityNotFoundError.Exists(typeof(Quiz), quizId);
            }
            var questionResult = await questionRepository.GetQuestion(questionId);
            if (questionResult.IsFailed)
            {
                return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);

            }
            await quizRepository.AddQuestionsToSectionQuizByQuizId(quizId, questionResult.Value);
            await unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> AddExistingQuestionToTask(int taskId, int questionId)
        {
            var pathTask = await pathTasksRepository.GetTaskForPathById(taskId);
            if (pathTask == null)
            {
                return EntityNotFoundError.Exists(typeof(PathTask), taskId);
            }
            var questionResult = await questionRepository.GetQuestion(questionId);
            if (questionResult.IsFailed)
            {
                return EntityNotFoundError.Exists(typeof(MultiChoiceQuestion), questionId);

            }
            await pathTasksRepository.AddQuestionsToTask(taskId, questionResult.Value);
            await unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> AddQuestionToSection(int id, CreatingQuestionModel model)
        {
            if (model == null)
                return BadRequestError.Exists(typeof(CreatingQuestionModel));
            var quiz = await quizRepository.GetQuizBySectionId(id);
            if (quiz == null)
                return EntityNotFoundError.Exists(typeof(Quiz), id);
            var question = mapper.Map<MultiChoiceQuestion>(model);
            var result = await quizRepository.AddQuestionsToSectionQuizBySectionId(id, question);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> AddQuestionToTask(int taskId, CreatingQuestionModel model)
        {
            if (model == null)
                return BadRequestError.Exists(typeof(CreatingQuestionModel));
            var pathTask = await pathTasksRepository.GetTaskForPathById(taskId);
            if (pathTask == null)
                return EntityNotFoundError.Exists(typeof(PathTask), taskId);
            var question = mapper.Map<MultiChoiceQuestion>(model);
            var result = await pathTasksRepository.AddQuestionsToTask(taskId, question);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> CreateModule(CreatingModuleModel model)
        {
            if (model == null)
                return BadRequestError.Exists(typeof(CreatingModuleModel));
            var module = mapper.Map<Module>(model);
            var result = await moduleRepository.GenerateModule(module);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> CreatePath(CreatingPathModel model)
        {
            if (model == null)
                return BadRequestError.Exists(typeof(CreatingPathModel));
            var path = mapper.Map<EducationalPath>(model);
            var result = await pathRepository.GenerateNewPath(path);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeleteQuestionFromSection(int id, int questionId)
        {
            var quiz = await quizRepository.GetQuizBySectionId(id);
            if (quiz == null)
                return BadRequestError.Exists(typeof(Quiz));
            var question = await questionRepository.GetQuestion(questionId);
            if (question == null)
                return BadRequestError.Exists(typeof(MultiChoiceQuestion));
            var result = await quizRepository.RemoveQuestionsFromSectionQuizBySectionId(id, questionId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeleteModule(int moduleId)
        {
            var module = await moduleRepository.GetModule(moduleId);
            if (module == null)
                return EntityNotFoundError.Exists(typeof(Module), moduleId);
            var result = await moduleRepository.DeleteModule(moduleId);
            if (result.IsFailed) return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeletePath(int pathId)
        {
            var module = await pathRepository.GetPath(pathId);
            if (module == null)
                return EntityNotFoundError.Exists(typeof(EducationalPath), pathId);
            var result = await pathRepository.DeletePath(pathId);
            if (result.IsFailed) return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeleteQuestionFromTask(int taskId, int questionId)
        {
            var pathTask = await pathTasksRepository.GetTaskForPathById(taskId);
            if (pathTask == null)
                return BadRequestError.Exists<PathTask>();
            var question = await questionRepository.GetQuestion(questionId);
            if (question == null)
                return BadRequestError.Exists(typeof(MultiChoiceQuestion));
            var result = await pathTasksRepository.RemoveQuestionsFromTask(taskId, questionId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeleteSection(int sectionId)
        {
            var section = await sectionsRepository.GetModuleSectionById(sectionId);
            if (section == null)
                return BadRequestError.Exists<ModuleSection>();
            var result = await sectionsRepository.DeleteModuleSection(sectionId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public Task<Result> DeleteTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GenerateSection(CreatingModuleSectionModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GenerateTask(CreatingPathTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateInstructorDetails(UpdateInstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateModuleDetails(int id, UpdateModuleModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdatePathDetails(int id, UpdatePathModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdatePathTask(int id, UpdatePathTaskModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateSectionDetails(int id, UpdatingModuleSectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
