
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Models;
using ScorecardMgm.Auth.Services.Interface;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Auth.Services.Implementation;



public class AuthServices : IAuthServices
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;

    public AuthServices(IUserRepository userRepo, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _configuration = configuration;
    }
    // private static string key { get; set;} 
    // private string key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }



    public async Task<Response> Login(UserLoginDto request)
    {
        try
        {
            // if (_userRepo.UserExists(request.Email))
            // {
            //     throw new Exception("User not found");
            // }
            var userFromRepo = await _userRepo.GetUserFromDB(request.Email);
            if (userFromRepo == null)
            {
                throw new Exception("User not found");
            }
            string pass = _userRepo.GetUserFromDB(request.Email).Result.PasswordHash;
            // _userRepo.GetPasswordHash(request.Email, pass);
            var HashPassword = Helpers.HelperMethods.VerifyPassword(request.Password, pass);

            if (!HashPassword)
                throw new Exception("Invalid Password");
            // var user = _userRepo.Login(request.Email, request.Password);
            else
            {
                var secret = _configuration.GetSection("AppSettings:Token").Value;
                var token = Helpers.HelperMethods.GenerateToken(secret);
                var res = new Response(token, request.Email);

                return res;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> ValidateToken(string token)
    {
        try
        {
            var secret = _configuration.GetSection("AppSettings:Token").Value;
            var result = Helpers.HelperMethods.ValidateToken(token, secret);
            return result;
        }
        catch (Exception ex)
        {
            return false;
        }
    }








    // private string GenerateToken()
    // {

    //     var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
    //     _configuration.GetSection("AppSettings:Token").Value));

    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    //     var token = new JwtSecurityToken(
    //         expires: DateTime.Now.AddDays(1),
    //         signingCredentials: creds);

    //     var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    //     return $"Token : {jwt} ";
    // }

    // TODO: create update, get user by id, get all users after implementing claims so that only admin can access these methods.



}