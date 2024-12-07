using Academic.Core.Abstractions;
using Academic.Core.Entities;
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
        private readonly IPathTasksRepository _pathTasksRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PathServices(IPathRepository pathRepository,
            IPathTasksRepository pathTasksRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._pathRepository = pathRepository;
            this._pathTasksRepository = pathTasksRepository;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<List<PathDto>> GetAllPaths(int page = 1, int size = 10)
        {
            var paths = await _pathRepository.GetPaths(page, size);
            
           
            return _mapper.Map<List<PathDto>>(paths);
        }

        public async Task<PathDto> GetPathById(int id)
        {
            var path = await _pathRepository.GetPath(id);
            
            if (path != null) 
                 return _mapper.Map<PathDto>(path);

            throw new EntityNotFoundException(typeof(Path), id);
        }

        public async Task<PathTaskModel> GetPathTask(int pathId)
        {

            var path = await _pathRepository.GetPath(pathId);

            if (path == null)
            {
                throw new EntityNotFoundException(typeof(EducationalPath), pathId); 
            }

            var pathTask = _pathTasksRepository.GetTaskForPathById(pathId);

            if (pathTask == null)
            {
                throw new EntityNotFoundException(typeof(PathTask), pathId); 
            }

            return _mapper.Map<PathTaskModel>(pathTask);

        }
    }
}
