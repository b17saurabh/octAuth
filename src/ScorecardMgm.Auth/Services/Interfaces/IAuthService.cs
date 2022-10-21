

using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Models;
using ScorecardMgm.Common.Entities;

namespace ScorecardMgm.Auth.Services.Interface
{
    public interface IAuthServices
    {
        Task<Response> Login(UserLoginDto request);
        Task<bool> ValidateToken(string token);

    }
}