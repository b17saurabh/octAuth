using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace ScorecardMgm.Common.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ScorecardMgmContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(ScorecardMgmContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<User> GetUserFromDB(string email)
        {
            // try
            // {
            // var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);


            var userFromDB = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            // if (user == null)
            //     throw new Exception("User not found");
            return userFromDB;
            // }
            // catch (Exception ex)
            // {
            //     throw new Exception(ex.Message);
            // }

        }

        public async Task<User> GetUserById(string userId)
        {
            User user = await _context.Users.FindAsync(userId);
            return user;
        }

        public async Task SaveToDb(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUser(User user)
        {
            var userFromDB = await GetUserById(user.id);
            user.PasswordHash = userFromDB.PasswordHash;

            try
            {
                _context.Entry(userFromDB).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // public bool UserExists(string email)
        // {
        //     var user = _context.Users.SingleOrDefault(x => x.Email == email);
        //     if (user != null)
        //         return true;

        //     return false;
        // }

        // public async Task<string> GetPasswordHash(string email, string pass)
        // {
        //     var user = _context.Users.SingleOrDefault(x => x.Email == email);
        //     if (user != null)
        //     {
        //         pass = user.PasswordHash;
        //         return user.PasswordHash;
        //     }
        //     return null;
        // }




    }
}