using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _db;

        public CategoryService(BlogDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> Deletecategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if(category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if(category != null)
            {
                return category;
            }

            return null!;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            if(category.CategoryId != 0)
            {
                var c = await _db.Categories.FindAsync(category.CategoryId);
                if(c != null)
                {
                    c.CategoryName = category.CategoryName;
                    await _db.SaveChangesAsync();
                    return category;
                }
            }

            return null!;
        }
    }
}