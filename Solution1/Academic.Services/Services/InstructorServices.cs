using System.IO;

namespace Academic.Services.Services
{
    public class InstructorServices
        (IQuestionRepository questionRepository, IModuleSectionsRepository sectionsRepository,
        IPathTasksRepository pathTasksRepository, IModuleRepository moduleRepository,
        IPathRepository pathRepository, IUnitOfWork unitOfWork,
        IQuizRepository quizRepository,IMapper mapper,
        IInstructorRepository instructorRepository, IUserIdentityRepository userRepository,
        AccountServicesHelpers accountServices)
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
            return result;
        }

        public async Task<Result> DeleteSection(int sectionId)
        {
            var result = await sectionsRepository.DeleteModuleSection(sectionId);
            if (result.IsFailed)
                return result;
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result> DeleteTask(int taskId)
        {
            var task = await pathTasksRepository.DeleteTask(taskId);
            if(task.IsFailed)
                return task;
            await unitOfWork.SaveChangesAsync();
            return task;
        }

        public async Task<Result> GenerateSection(CreatingModuleSectionModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var section = new ModuleSection
            {
                Body = model.Body,
                ModuleId = model.ModuleId,
                Title = model.Title,
                Quiz = new Quiz(),
            };
            var sectionResult = await sectionsRepository.GenerateNewModuleSectionInModule(section);
            if (sectionResult.IsFailed)
                return sectionResult;
            await unitOfWork.SaveChangesAsync();
            return sectionResult;
        }

        public async Task<Result> GenerateTask(CreatingPathTaskModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var newTask = new PathTask
            {
                Description = model.Description,
                PathId = model.PathId,
                Title = model.Title,
                MinPercentagesToCertify = model.MinPercentagesToCertify,
            };
            foreach(var questiondDto in model.Questions)
            {
                var question = new MultiChoiceQuestion
                {
                    Answer = questiondDto.Answer,
                    ChoiceA = questiondDto.ChoiceA,
                    ChoiceB = questiondDto.ChoiceB,
                    ChoiceC = questiondDto.ChoiceC,
                    ChoiceD = questiondDto.ChoiceD,
                    Content = questiondDto.Content,
                    InstructorId = questiondDto.InstructorId,
                    Points = questiondDto.Points,
                };
                var result = await questionRepository.GenerateNewQuestion(question);
                if (result.IsFailed) return result;
            }
            newTask.TotalPoints = model.Questions.Sum(q => q.Points);
            var pathTaskResult = await pathTasksRepository.GenerateTaskForPath(newTask);
            if (pathTaskResult.IsFailed)
                return pathTaskResult;
            await unitOfWork.SaveChangesAsync();
            return pathTaskResult;
        }

        //public Task<Result> UpdateInstructorDetails(UpdateInstructorModel model)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Result> UpdateModuleDetails(int id, UpdateModuleModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            var moduleResult = await moduleRepository.GetModule(id);
            if (moduleResult is null)
                return EntityNotFoundError.Exists<Module>(id);
            moduleResult.Difficulty = model.Difficulty is null ? moduleResult.Difficulty : (double)model.Difficulty;
            moduleResult.Description = model.Description is null ? moduleResult.Description : model.Description;
            moduleResult.Title = model.Title is null ? moduleResult.Title : model.Title;

            var updateResult = await moduleRepository.UpdateModule(moduleResult);
            if (updateResult.IsFailed)
                return updateResult;
            await unitOfWork.SaveChangesAsync();
            return updateResult;
        }

        public async Task<Result> UpdatePathDetails(int id, UpdatePathModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));
            var path = await pathRepository.GetPath(id);
            if (path is null)
                return EntityNotFoundError.Exists<EducationalPath>(id);
            path.Difficulty = model.Difficulty is null ? path.Difficulty : (double)model.Difficulty;
            path.Description = model.Description is null ? path.Description : model.Description;
            path.Title = model.Title is null ? path.Title : model.Title;
            path.IntroductionBody = model.IntroductionBody is null ? path.IntroductionBody : model.IntroductionBody;

            var updateResult = await pathRepository.UpdatePath(path);
            if (updateResult.IsFailed)
                return updateResult;
            await unitOfWork.SaveChangesAsync();
            return updateResult;
        }

        public async Task<Result> UpdatePathTask(int id, UpdatePathTaskModel model)
        {
            var pathTaskResult = await pathTasksRepository.GetTaskForPathById(id);
            if (pathTaskResult.IsFailed)
                return Result.Fail(pathTaskResult.Errors);
            var pathTask = pathTaskResult.Value;
            pathTask.Description = model.Description is null ? pathTask.Description : model.Description;
            pathTask.Title = model.Title is null ? pathTask.Title : model.Title;
            pathTask.MinPercentagesToCertify = model.MinPercentagesToCertify is null ? pathTask.MinPercentagesToCertify : (double)model.MinPercentagesToCertify;

            var totalScore = pathTask.TotalPoints;
            if(model.Questions is not null)
            {
                foreach (var questionDto in model.Questions)
                {
                    if(questionDto.Id is null)
                    {
                        if (questionDto.WantDelete)
                        {
                            var questionResult = await questionRepository.GetQuestion((int)questionDto.Id!);
                            if (questionResult.IsFailed)
                                return Result.Fail(questionResult.Errors);
                            var result = await DeleteQuestionFromTask(id, (int)questionDto.Id);
                            if (result.IsFailed)
                                return Result.Fail(result.Errors);
                            totalScore -= questionResult.Value.Points;
                        }
                        else
                        {
                            var question = mapper.Map<CreatingQuestionModel>(questionDto);
                            var questionResult = await AddQuestionToTask(id, question);
                            if (questionResult.IsFailed)
                                return Result.Fail(questionResult.Errors);
                            totalScore += question.Points;
                        }
                    }
                    else
                    {
                        var questionResult = await questionRepository.GetQuestion((int)questionDto.Id);
                        if (questionResult.IsFailed)
                            return Result.Fail(questionResult.Errors);
                        var question = questionResult.Value;

                        question.Answer = questionDto.Answer is null ? question.Answer : (char)questionDto.Answer;
                        question.Content = questionDto.Content is null ? question.Content : questionDto.Content;
                        question.ChoiceA = questionDto.ChoiceA is null ? question.ChoiceA : questionDto.ChoiceA;
                        question.ChoiceB = questionDto.ChoiceB is null ? question.ChoiceB : questionDto.ChoiceB;
                        question.ChoiceC = questionDto.ChoiceC is null ? question.ChoiceC : questionDto.ChoiceC;
                        question.ChoiceD = questionDto.ChoiceD is null ? question.ChoiceD : questionDto.ChoiceD;
                        question.Points = questionDto.Points is null ? question.Points : (double)questionDto.Points;
                        
                        await questionRepository.UpdateQuestion(question);

                        totalScore += question.Points;
                    }
                }
            }
            var updatingResult = await pathTasksRepository.UpdateTask(pathTask);
            if (updatingResult.IsFailed)
                return Result.Fail(updatingResult.Errors);
            await unitOfWork.SaveChangesAsync();
            return updatingResult;
        }

        public async Task<Result> UpdateSectionDetails(int id, UpdatingModuleSectionModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            var section = await sectionsRepository.GetModuleSectionById(id);
            if (section is null)
                return EntityNotFoundError.Exists<ModuleSection>(id);
            section.Body = model.Body is null ? section.Body : model.Body;
            section.Title = model.Title is null ? section.Title : model.Title;

            var updateResult = await sectionsRepository.UpdateModuleSection(section);
            if (updateResult.IsFailed)
                return updateResult;
            await unitOfWork.SaveChangesAsync();
            return updateResult;
        }

        public async Task<Result> CreateNewInstructor(ConfirmAccountFromInstructorModel model)
        {
            var account = await userRepository.GetByEmail(model.Email);
            if (account == null)
                return EntityNotFoundError.Exists(typeof(Instructor), model.Email);
            var instructor = (Instructor)account;
            if (instructor.ConfirmationToken != model.Token)
                return UnauthorizedError.Exists(model.Email);

            var salt = accountServices.CreateSalt();
            var password = accountServices.HashPasswordWithSalt(salt, model.Password);
            instructor.Password = password;
            instructor.Salt = salt;
            instructor.PasswordIsSet = true;
            await instructorRepository.UpdateInstructorDetails(instructor);
            await unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
