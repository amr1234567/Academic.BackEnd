using Academic.Core.Abstractions;
using Academic.Core.Exceptions;
using Academic.Repository.Repositories;
using Academic.Services.Abstractions;
using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Services
{
    public class PathServices : IPathServices
    {
        private readonly IPathRepository _pathRepository;
        private readonly IMapper _mapper;

        public PathServices(IPathRepository pathRepository,IPathTasksRepository pathTasksRepository, IMapper mapper)
        {
            this._pathRepository = pathRepository;
            this._mapper = mapper;
        }
        public async Task<List<PathDto>> GetAllPaths(int page = 1, int size = 10)
        {
            var paths = await _pathRepository.GetPaths(page, size);

            return _mapper.Map<List<PathDto>>(paths);
        }

        public async Task<PathDto> GetPathById(int id)
        {
            var path = await _pathRepository.GetPath(id);

            if(path != null) 
                 return _mapper.Map<PathDto>(path);

            throw new EntityNotFoundException(typeof(Path), id);
        }

        public Task<PathTaskModel> GetPathTask(int pathId)
        {
            throw new NotImplementedException();
        }
    }
}
