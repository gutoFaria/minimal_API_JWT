using backend.Models;

namespace backend.Services
{
    public interface IPostService
    {
        Task<Post> CreatePost(Post post);
        Task<Post> GetPostById(int id);
        Task<IEnumerable<Post>> GetAllPost();
        Task<Post> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}