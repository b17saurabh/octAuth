
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Service.Interface;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Auth.Service.Implementation;



public class AuthServices : IAuthServices
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _configuration;

    public AuthServices(IUserRepository userRepo, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _configuration = configuration;
    }
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

    public async Task Register(UserDto request)
    {
        //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        // Email verification if exists or not else throw exception
        // if (await _context.Users.FindAsync(request.Email) != null)
        // {
        //     throw new Exception("Email already exists");
        // }
        try
        {
            var userFromRepo = await _userRepo.GetUserAsync(request.Email);
            if (userFromRepo != null)
            {
                throw new Exception("User already exists");
            }


            var user = new User
            {
                id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = PasswordHasher(request.Password).Result,
                Role = 0
            };
            _userRepo.Register(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<string> Login(UserLoginDto request)
    {
        try
        {
            // if (_userRepo.UserExists(request.Email))
            // {
            //     throw new Exception("User not found");
            // }
            var userFromRepo = _userRepo.GetUserAsync(request.Email);
            if (userFromRepo == null)
            {
                throw new Exception("User already exists");
            }
            string pass = "";
            _userRepo.GetPasswordHash(request.Email, pass);
            var HashPassword = BCrypt.Net.BCrypt.Verify(request.Password, pass);

            if (!HashPassword)
                throw new Exception("Invalid Password");
            var user = _userRepo.Login(request.Email, request.Password);
            var token = GenerateToken();
            return token;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> PasswordHasher(string password)
    {
        return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password, 12));
    }

    public string GenerateToken()
    {

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }


}