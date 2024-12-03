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
    public class AdminServices : IAdminServices
    {
        public Task<int> AcceptModule(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<int> AcceptPath(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<int> BlockInstructor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteInstructor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CreatingInstructorDto> GenerateInstructor(CreateInstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<PathDto>> GetAllPathsNeverGotResponse(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<int> RejectModule(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RejectPath(int pathId)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorDto> UpdateInstructorDetails(UpdateInstructorForAdminModel model)
        {
            throw new NotImplementedException();
        }
    }
}
