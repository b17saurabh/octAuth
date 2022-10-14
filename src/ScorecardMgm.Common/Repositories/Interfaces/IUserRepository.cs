using ScorecardMgm.Common.Entities;

namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Login(string email, string password);
        Task Register(User user);
        // bool UserExists(string email);


        Task<User> GetUserAsync(string email);
        Task<string> GetPasswordHash(string email, string pass);
    }
}