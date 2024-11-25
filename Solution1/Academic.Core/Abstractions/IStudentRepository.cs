using Academic.Core.Identitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Core.Abstractions
{
    internal interface IStudentRepository
    {
        Task<int> RollInOrOutModule(int studentId, int moduleId, bool rollIn = true);
        Task<int> RollInOrOutPath(int studentId, int pathId, bool rollIn = true);
        Task<User> UpdateDetails(User user);
        Task<int> BlockUser(int userId);
        
    }
}
