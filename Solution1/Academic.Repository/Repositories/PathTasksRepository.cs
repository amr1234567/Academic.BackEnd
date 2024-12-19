using Academic.Core.Entities;
using Academic.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Academic.Core.Errors;
using Academic.Core.Identitiy;
using Academic.Core.Entities.ManyToManyEntities;

namespace Academic.Repository.Repositories
{
    public class PathTasksRepository : IPathTasksRepository
    {
        private readonly ApplicationDbContext _context;

        public PathTasksRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        
        public async Task<Result> GenerateTaskForPath(PathTask path)
        {
            var checkPath = await GetTaskForPathByPathId(path.PathId);
            if (checkPath != null)
                return EntityExistsError.Exists<PathTask>();
            await _context.PathTasks.AddAsync(path);

            return Result.Ok();
        }

        public async Task<Result> UpdateTask(PathTask path)
        {
            if (path == null)
                return Result.Fail(new Error("Argument can't be null"));
            _context.PathTasks.Update(path);

            return Result.Ok();
        }

        public async Task<Result> DeleteTask(int pathId)
        {
            var task = await _context.PathTasks.FirstOrDefaultAsync(t => t.PathId == pathId);
            if (task == null)
                return EntityNotFoundError.Exists<PathTask>(pathId);
            _context.PathTasks.Remove(task);

            return Result.Ok();
        }

        public async Task<Result<PathTask>> GetTaskForPathByPathId(int pathId)
        {
            var task = await _context.PathTasks.AsNoTracking().Where(t => t.PathId == pathId).Include(p => p.Questions)
                .FirstOrDefaultAsync();

            if(task != null)
                return task;

            return EntityNotFoundError.Exists<PathTask>(pathId);
        }

        public async Task<Result<PathTask>> GetTaskForPathById(int taskId)
        {
            var task = await _context.PathTasks.AsNoTracking().Where(t => t.PathId == taskId).Include(p => p.Questions)
                .FirstOrDefaultAsync();

            if (task != null)
                return task;

            return EntityNotFoundError.Exists<PathTask>(taskId);
        }

        public async Task<List<PathTask>> GetPathTasks(int page = 1, int size = 10)
        {
            return await _context.PathTasks
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Result> AddQuestionsToTask(int taskId, params int[] questionIds)
        {
            if (questionIds == null || questionIds.Length == 0)
                return EntityNotFoundError.Exists("No QuestionIds provided");


            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return EntityNotFoundError.Exists(typeof(PathTask), taskId);

            var questions = await _context.MultiChoiceQuestions
                .Where(q => questionIds.Contains(q.Id))
                .ToListAsync();

            foreach (var question in questions)
                task.Questions.Add(question);

            task.Questions = task.Questions.Distinct().ToList();
        
            return Result.Ok();
        }

        public async Task<Result> AddQuestionsToTask(int taskId, params MultiChoiceQuestion[] questions)
        {
            if (questions == null || questions.Length == 0)
                return EntityNotFoundError.Exists("No Questions provided");

            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return EntityNotFoundError.Exists(typeof(PathTask), taskId);


            foreach (var question in questions)
                task.Questions.Add(question);

            task.Questions = task.Questions.Distinct().ToList();

            return Result.Ok();
        }

        public async Task<Result> RemoveQuestionsFromTask(int taskId, params int[] questionIds)
        {
            
            if (questionIds == null || questionIds.Length == 0)
                return BadRequestError.Exists($"{nameof(questionIds)} must be provided");

            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return EntityNotFoundError.Exists(typeof(PathTask), taskId);

            var questions = await _context.MultiChoiceQuestions
                .Where(q => questionIds.Contains(q.Id))
                .ToListAsync();

            if (questions == null || questions.Count == 0)
                return EntityNotFoundError.Exists("No Questions Found for these ids");

            foreach (var question in questions)
                task.Questions.Remove(question);

            return Result.Ok();
        }

        public async Task<Result<List<PathTaskUsers>>> GetPathTasksCompletedForUser(int userId, int page, int size)
        {
            var pathTasks = await _context.PathTaskUsers.Where(u => u.UserId == userId)
                .Include(p => p.PathTask).Take((page - 1) * size).Take(size).ToListAsync();
            if (pathTasks == null)
                return EntityNotFoundError.Exists<PathTaskUsers>("");

            return pathTasks;
        }

        public async Task<Result> SolveTask(PathTaskUsers userTask)
        {
            ArgumentNullException.ThrowIfNull(nameof(userTask));
            var checkUserWithTask = await _context.PathTaskUsers.AsNoTracking().FirstOrDefaultAsync(p => p.PathTaskId == userTask.PathTaskId && p.UserId == userTask.UserId);
            if (checkUserWithTask != null)
                return EntityExistsError.Exists<PathTaskUsers>();
            await _context.PathTaskUsers.AddAsync(userTask);
            return Result.Ok();
        }
    }
}
