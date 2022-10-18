using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ScorecardMgm.Auth.Helpers;

public static class HelperMethods
{
    // public HelperMethods(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }
    public static string GenerateToken(string secret)
    {

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return $"Token : {jwt} ";
    }

    public static async Task<string> PasswordHasher(string password)
    {
        return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password, 12));
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

}