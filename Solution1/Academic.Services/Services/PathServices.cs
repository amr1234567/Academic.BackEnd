using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class PathServices : IPathServices
    {
        public Task<List<PathDto>> GetAllPaths(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<PathDto> GetPathById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PathTaskModel> GetPathTask(int pathId)
        {
            throw new NotImplementedException();
        }
    }
}
