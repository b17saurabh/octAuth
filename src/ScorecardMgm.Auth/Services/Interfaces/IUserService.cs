

using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Common.Entities;

namespace ScorecardMgm.Auth.Services.Interface
{
    public interface IUserServices
    {
        Task Register(UserDto request);

        Task<User> DeleteUser(string userId);

        Task<User> GetUserById(string userId);

        Task<User> UpdateUser(string userId, UserUpdate request);

        Task<List<User>> GetAllUsers();




    }
}