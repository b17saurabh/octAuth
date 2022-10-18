using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Services.Interface;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Auth.Services.Implementation;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> DeleteUser(string userId)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteUser(user);
        return user;
    }

    public Task<List<User>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public async Task<User> GetUserById(string userId)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user;
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
            var userFromRepo = await _userRepository.GetUserFromDB(request.Email);
            if (userFromRepo != null)
            {
                throw new Exception("User already exists");
            }


            var user = new User
            {
                id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = Helpers.HelperMethods.PasswordHasher(request.Password).Result,
                // Role = 0
            };
            await _userRepository.SaveToDb(user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<User> UpdateUser(string userId, UserUpdate request)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.id = userId;
        user.Name = request.Name;
        user.Email = request.Email;
        await _userRepository.UpdateUser(user);
        return user;


    }
}
