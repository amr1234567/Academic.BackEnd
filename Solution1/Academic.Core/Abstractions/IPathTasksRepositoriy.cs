﻿using Academic.Core.Entities;
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
        Task<PathTask> GenerateTaskForPath(PathTask path);
        Task<PathTask> UpdateTask(PathTask path);
        Task<PathTask> DeleteTask(int pathId);
        Task<PathTask> GetTaskForPathByPathId(int pathId);
        Task<PathTask> GetTaskForPathById(int taskId);
        Task<List<PathTask>> GetPathTasks(int page = 1, int size = 10);

        Task<Result> AddQuestionsToTask(int taskId, params int[] questionId);
        Task<Result> AddQuestionsToTask(int taskId, params MultiChoiceQuestion[] question);

        Task<Result> RemoveQuestionsFromTask(int taskId, params int[] questionId);
    }
}
