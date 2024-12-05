using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class PathTasksRepository : IPathTasksRepository
    {
        public Task<int> AddQuestionsToTask(int taskId, params int[] questionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddQuestionsToTask(int taskId, params MultiChoiceQuestion[] question)
        {
            throw new NotImplementedException();
        }

        public Task<PathTask> DeleteTask(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<PathTask> GenerateTaskForPath(PathTask path)
        {
            throw new NotImplementedException();
        }

        public Task<List<PathTask>> GetPathTasks(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PathTask> GetTaskForPathById(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<PathTask> GetTaskForPathByPathId(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveQuestionsFromTask(int taskId, params int[] questionId)
        {
            throw new NotImplementedException();
        }

        public Task<PathTask> UpdateTask(PathTask path)
        {
            throw new NotImplementedException();
        }
    }
}
