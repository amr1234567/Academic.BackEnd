using Academic.Core.Entities;
using Academic.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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

        public async Task<int> AddQuestionsToTask(int taskId, params int[] questionId)
        {
            if (questionId == null)
            {
                throw new ArgumentException("No questionIds Added");
            }

            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }

            var questions = await _context.MultiChoiceQuestions
                .Where(q => questionId.Contains(q.Id))
                .ToListAsync();

            int addedCount = 0;
            foreach (var question in questions)
            {
                if (!task.Questions.Contains(question))
                {
                    task.Questions.Add(question);
                    addedCount++;
                }
            }

            return addedCount;
        }

        public async Task<int> AddQuestionsToTask(int taskId, params MultiChoiceQuestion[] questions)
        {
            if (questions == null )
            {
                throw new ArgumentException("No questions Added");
            }

            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }

            int addedCount = 0;
            foreach (var question in questions)
            {
                if (!task.Questions.Contains(question))
                {
                    task.Questions.Add(question);
                    addedCount++;
                }
            }

            return addedCount;
        }

        public async Task<int> RemoveQuestionsFromTask(int taskId, params int[] questionId)
        {
            if (questionId == null )
            {
                throw new ArgumentException("No questionIds Added");
            }

            var task = await _context.PathTasks
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentException("Task not found.");
            }

            var questions = await _context.MultiChoiceQuestions
                .Where(q => questionId.Contains(q.Id))
                .ToListAsync();

            int removedCount = 0;
            foreach (var question in questions)
            {
                if (task.Questions.Contains(question))
                {
                    task.Questions.Remove(question);
                    removedCount++;
                }
            }

            return removedCount;
        }
    }
}
