

using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Common.Entities;

namespace ScorecardMgm.Auth.Services.Interface
{
    public interface IAuthServices
    {
        Task<string> Login(UserLoginDto request);

    }
}