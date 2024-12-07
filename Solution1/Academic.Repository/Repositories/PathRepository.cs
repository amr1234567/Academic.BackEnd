using Academic.Core.Abstractions;
using Academic.Core.Entities;
using Academic.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Repository.Repositories
{
    public class PathRepository : IPathRepository
    {
        private readonly ApplicationDbContext _context;

        public PathRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> GenerateNewPath(EducationalPath path)
        {
            var ok = 1;
            await _context.Paths.AddAsync(path);
            
            return ok;
        }

        public async Task<EducationalPath> DeletePath(int pathId)
        {
            var path = await _context.Paths.FindAsync(pathId);
            if (path != null)
                _context.Paths.Remove(path);

            return path;
        }

        public async Task<EducationalPath> GetPath(int pathId)
        {
            var path = await _context.Paths
                .FirstOrDefaultAsync(p => p.Id == pathId);
            if(path != null) 
                return path;

            return null;
        }

        // Get a list of paths with pagination (without saving)
        public async Task<List<EducationalPath>> GetPaths(int page = 1, int size = 15)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 15;

            return await _context.Paths
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<EducationalPath>> GetPathsForInstructor(int instructorId, int page = 1, int size = 15)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 15;

            return await _context.Paths
                .Where(p => p.InstructorId == instructorId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<EducationalPath>> GetPathsForUser(int userId, int page = 1, int size = 15)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 15;

            return await _context.Paths
                .Where(p => p.Id == userId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<EducationalPath> UpdatePath(EducationalPath path)
        {
            var newPathData = await _context.Paths.FindAsync(path.Id);
            if (newPathData != null)
            {
                newPathData.Title = path.Title;
                newPathData.Description = path.Description;
                newPathData.IntroductionBody = path.IntroductionBody;
                newPathData.Difficulty = path.Difficulty;
                newPathData.NumOfModules = path.NumOfModules;
                newPathData.InstructorId = path.InstructorId;
                newPathData.PathTaskId = path.PathTaskId;

                
                return newPathData;
            }
            return null;
        }
    }
}
