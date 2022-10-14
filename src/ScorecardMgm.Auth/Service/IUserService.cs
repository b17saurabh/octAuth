

using ScorecardMgm.Auth.Contract;

namespace ScorecardMgm.Auth.Service.Interface
{
    public interface IAuthServices
    {
        Task<string> Login(UserLoginDto request);
        Task Register(UserDto request);


    }
}