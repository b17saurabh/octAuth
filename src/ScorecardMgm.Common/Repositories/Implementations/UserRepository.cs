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

        public async Task Login(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                    throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task Register(User user)
        {

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public bool UserExists(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            if (user != null)
                return true;

            return false;
        }

        public async Task<string> GetPasswordHash(string email, string pass)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            if (user != null)
            {
                pass = user.PasswordHash;
                return user.PasswordHash;
            }
            return null;
        }




    }
}