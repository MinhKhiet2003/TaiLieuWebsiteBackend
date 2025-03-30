using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.User).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.User)
                                            .FirstOrDefaultAsync(c => c.category_id == id);
        }


        public async Task AddCategoryAsync(Category category)
        {

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories
                    .FindAsync(category.category_id);

                if (existingCategory == null)
                    return false;

                existingCategory.name = category.name;
                existingCategory.description = category.description;
                existingCategory.class_id = category.class_id;
                existingCategory.uploaded_by = category.uploaded_by;
                existingCategory.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error while updating category", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category", ex);
            }
        }


        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Category>> SearchCategoriesAsync(string keyword)
        {
            return await _context.Categories.Include(c => c.User)
                .Where(c => c.name.ToLower().Contains(keyword.ToLower()))
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Category>> GetCategoriesByClassIdAsync(int classId)
        {
            return await _context.Categories.Include(c => c.User)
                .Where(c => c.class_id == classId)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAndClassAsync(string name, int classId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.name.ToLower() == name.ToLower() && c.class_id == classId);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Class>> GetUsedClassesAsync()
        {
            return await _context.Categories
                .Select(c => c.Class)
                .Distinct()
                .ToListAsync();
        }
        public async Task<Dictionary<int, int>> CountCategoriesByClassAsync()
        {
            return await _context.Categories
                .GroupBy(c => c.class_id)
                .Select(g => new { ClassId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.ClassId, g => g.Count);
        }
    }
}
