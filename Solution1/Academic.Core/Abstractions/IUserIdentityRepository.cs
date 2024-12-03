using Academic.Core.Base;

namespace Academic.Core.Abstractions
{  
    public interface IUserIdentityRepository
    {
        Task<IdentityUser?> GetById(int id);
        Task<IdentityUser?> GetByEmail(string email);

    }
}
