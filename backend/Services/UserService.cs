using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _db;

        public UserService(BlogDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetAsync(UserLogin userLogin)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(o => o.UserName.Equals(userLogin.UserName,StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

            return user!;
        }
    }
}