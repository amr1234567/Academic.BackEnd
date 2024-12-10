using Academic.Core.Entities;
using Academic.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Academic.Core.Errors;

namespace Academic.Repository.Repositories
{
    public class PathTasksRepository : IPathTasksRepository
    {
        private readonly ApplicationDbContext _context;

        public PathTasksRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        
        public async Task<PathTask> GenerateTaskForPath(PathTask path)
        {
            await _context.PathTasks.AddAsync(path);
             
            return path;
        }

        public async Task<PathTask> UpdateTask(PathTask path)
        {
            _context.PathTasks.Update(path);
             
            return path;
        }

        public async Task<PathTask> DeleteTask(int pathId)
        {
            var task = await _context.PathTasks.FirstOrDefaultAsync(t => t.PathId == pathId);

            _context.PathTasks.Remove(task);
             
            return task;
        }

        public async Task<PathTask> GetTaskForPathByPathId(int pathId)
        {
            var task = await _context.PathTasks
                .FirstOrDefaultAsync(t => t.PathId == pathId);

            if(task != null)
                return task;

            return null;
        }

        public async Task<PathTask> GetTaskForPathById(int taskId)
        {
            var task = await _context.PathTasks
                .FirstOrDefaultAsync(t => t.PathId == taskId);

            if (task != null)
                return task;

            return null;
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
    }
}
