using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(UserLogin userLogin);
    }
}