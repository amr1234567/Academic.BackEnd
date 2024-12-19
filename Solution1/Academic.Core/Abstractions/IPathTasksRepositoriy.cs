using Academic.Core.Entities;
using Academic.Core.Entities.ManyToManyEntities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IPathTasksRepository
    {
        Task<Result> GenerateTaskForPath(PathTask path);
        Task<Result> UpdateTask(PathTask path);
        Task<Result> DeleteTask(int pathId);
        Task<Result<PathTask>> GetTaskForPathByPathId(int pathId);
        Task<Result<PathTask>> GetTaskForPathById(int taskId);
        Task<List<PathTask>> GetPathTasks(int page = 1, int size = 10);

        Task<Result> AddQuestionsToTask(int taskId, params int[] questionId);
        Task<Result> AddQuestionsToTask(int taskId, params MultiChoiceQuestion[] question);

        Task<Result> RemoveQuestionsFromTask(int taskId, params int[] questionId);
        Task<Result<List<PathTaskUsers>>> GetPathTasksCompletedForUser(int userId, int page, int size);
        Task<Result> SolveTask(PathTaskUsers userTask);
    }
}
