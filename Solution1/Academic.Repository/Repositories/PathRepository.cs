using Academic.Core.Abstractions;
using Academic.Core.Entities;
using Academic.Core.Identitiy;
using Academic.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            await _context.Paths.AddAsync(path);
            return await _context.SaveChangesAsync();
        }
        public async Task<EducationalPath> DeletePath(int pathId)
        {
            var path = await _context.Paths.FindAsync(pathId);

            if(path != null)
            {
                // Remove 
                _context.Paths.Remove(path);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<EducationalPath> GetPath(int pathId)
        {
            
            var path = await _context.Paths
                .FirstOrDefaultAsync(p => p.Id == pathId);

            if(path != null) 
                return path;

            return null;
        }

        public async Task<List<EducationalPath>> GetPaths(int page = 1, int size = 15)
        {
            if(page <= 0)
                page = 1;
            if(size <= 0)
                size = 15;

            return await _context.Paths
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<EducationalPath>> GetPathsForInstructor(int instructorId, int page = 1, int size = 15)
        {
            if (page <= 0)
                page = 1;
            if (size <= 0)
                size = 15;

            return await _context.Paths
                .Where(p => p.Id == instructorId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<EducationalPath>> GetPathsForUser(int userId, int page = 1, int size = 15)
        {
            if (page <= 0)
                page = 1;
            if (size <= 0)
                size = 15;
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

                _context.Paths.Update(newPathData);
                await _context.SaveChangesAsync();
                return newPathData; // Return the updated path
            }

            return null;
        }

        public Task<List<EducationalPath>> GetPathsWithCriteria(Func<EducationalPath, bool> criteria, int page = 1, int size = 15)
        {

            return Task.FromResult(
                    _context.Paths.AsNoTracking()
                        .Where(criteria).Skip((page - 1) * size)
                        .Take(size).ToList()
                );
        }

        public Task<List<EducationalPath>> GetPathsWithModulesAndInstructorWithCriteria(Func<EducationalPath, bool> criteria, int page = 1, int size = 15)
        {
            var pathsWithModules = _context.Paths.AsNoTracking().Include(p => p.Modules)
                        .Where(criteria)
                       .Skip((page - 1) * size)
                       .Take(size).ToList();
            return Task.FromResult(pathsWithModules);
        }

        public async Task<List<EducationalPath>> GetPathsWithModules(int page = 1, int size = 15)
        {
            var pathsWithModules = await _context.Paths.AsNoTracking().Include(p => p.Modules)
                        .Skip((page - 1) * size)
                        .Take(size).ToListAsync();
            return pathsWithModules;
        }
    }
}
