using Academic.Services.Models.Inputs;
using Academic.Services.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Services.Abstractions
{
    public interface IAdminServices
    {
        Task<Result<CreatingInstructorDto>> GenerateInstructor(CreateInstructorModel model);
        Task<int> DeleteInstructor(int id);
        Task<int> BlockInstructor(int id);
        Task<InstructorDto> UpdateInstructorDetails(UpdateInstructorForAdminModel model);

        Task<List<PathDto>> GetAllPathsNeverGotResponse(int page = 1, int size = 10);
        Task<int> AcceptPath(int pathId);
        Task<int> RejectPath(int pathId);
        Task<int> AcceptModule(int moduleId);
        Task<int> RejectModule(int moduleId);
        Task<Result> GenerateAdmin(CreateAdminModel model);
    }
}
