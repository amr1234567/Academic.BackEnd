using Academic.Core.Base;

namespace Academic.Core.Abstractions
{  
    public interface IUserIdentityRepository
    {
        Task<IdentityUser?> GetById(int id);
        Task<IdentityUser?> GetByEmail(string email);
        Task<int> UpdateByCriteriaWithFunc(Func<IdentityUser, bool> criteria, Action<IdentityUser> action);
        Task<IdentityUser> UpdateUser(IdentityUser identity);
        Task<int> UpdateAllWithFunc(Action<IdentityUser> action);
        Task<IdentityUser?> GetUserByCriteria(Func<IdentityUser, bool> criteria);
    }
}
