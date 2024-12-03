using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IPathServices
    {
        Task<List<PathDto>> GetAllPaths(int page = 1, int size = 10);
        Task<PathDto> GetPathById(int id);
        Task<PathTaskModel> GetPathTask(int pathId);
        
    }
}
