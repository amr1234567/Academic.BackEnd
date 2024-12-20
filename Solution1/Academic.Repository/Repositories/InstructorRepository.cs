﻿using Academic.Core.Abstractions;
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
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserIdentityRepository userIdentityRepository;

        // DI
        public InstructorRepository(ApplicationDbContext context, IUserIdentityRepository userIdentityRepository)
        {
            this._context = context;
            this.userIdentityRepository = userIdentityRepository;
        }

        public async Task<int> GenerateNewInstructor(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            return instructor.Id;
        }

        public async Task<int> DeleteInstructor(int instructorId)
        {
            // Find instructor
            var instructor = await _context.Instructors.FindAsync(instructorId);

            if(instructor != null)
            {
                // Remove
                _context.Instructors.Remove(instructor);
                return await _context.SaveChangesAsync();
            }

            // not found
            return 0;
        }

        public async Task<int> BlockInstructor(int instructorId)
        {
            // Find instructor
            var instructor = await _context.Instructors.FindAsync(instructorId);

            if (instructor != null)
            {
                // Block instructor
                instructor.IsActive = false;
                _context.Instructors.Update(instructor);
                return await _context.SaveChangesAsync();
            }

            // Not found
            return 0;
        }

        public async Task<List<Instructor>> GetInstructors(int page = 1, int size = 10)
        {
            // Validation 
            if (page <= 0)
                page = 1;

            if (size <= 0)
                size = 10;


            return await _context.Instructors
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            // another way by using (X.PagedList) 
        }

        public async Task<Instructor> UpdateInstructorDetails(Instructor instructor)
        {
             var newInstructorData = await _context.Instructors.FirstOrDefaultAsync(i=> i.Id == instructor.Id);

            if(newInstructorData != null)
            {
                await userIdentityRepository.UpdateUser(instructor);
                newInstructorData.HashedPassword = instructor.HashedPassword;
                newInstructorData.Title = instructor.Title;
                newInstructorData.PasswordIsSet = instructor.PasswordIsSet;
                newInstructorData.ConfirmationToken = instructor.ConfirmationToken;
                    
                return newInstructorData;
            }

            return null;
        }
    }
}
