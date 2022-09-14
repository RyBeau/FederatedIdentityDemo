using AuthAPI.Responses;
using AuthAPI.Services.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Services.DB
{
    public class Repository
    {
        private Context _dbContext;

        public Repository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserSummary> GetUser(string username, string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return _dbContext.Users
                    .Where(s => s.Username == username && 
                        s.Password == Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))))
                    .Join(_dbContext.Roles,
                        s => s.Role,
                        r => r.Id,
                        (s, r) => new UserSummary
                        {
                            username = s.Username,
                            role = r.Name
                        })
                    .SingleAsync();
            }
        }

           

        public Task<Role> GetRole(int roleId)
        {
            return _dbContext.Roles
                .Where(r => r.Id == roleId)
                .SingleAsync();
        }
    }
}
