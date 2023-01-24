using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _db;

        public PostService(BlogDbContext db)
        {
            _db = db;
        }

        public async Task<Post> CreatePost(Post post)
        {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if(post != null)
            {
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _db.Posts.ToListAsync()!;
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if(post != null)
            {
                return post;
            }

            return null!;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            if(post.PostId != 0)
            {
                var p = await _db.Posts.FindAsync(post.PostId);
                if(p != null)
                {
                    p.Title = post.Title;
                    p.ImageUrl = post.ImageUrl;
                    p.Description = post.Description;
                    p.PublishDate = post.PublishDate;
                    p.CategoryId = post.CategoryId;

                    await _db.SaveChangesAsync();
                    return post;
                }
            }

            return null!;
        }

    }
}