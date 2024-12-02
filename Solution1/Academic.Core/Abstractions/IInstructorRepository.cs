using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    public interface IInstructorRepository
    {
        Task<int> GenerateNewInstructor(Instructor instructor);
        Task<int> DeleteInstructor(int instructorId);
        Task<int> BlockInstructor(int instructorId);
        Task<Instructor> UpdateInstructorDetails(Instructor instructor);
        Task<List<Instructor>> GetInstructors(int page = 1, int size = 10); 
    }
}
