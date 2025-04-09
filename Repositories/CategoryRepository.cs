using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Dtos;
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

        public async Task<IEnumerable<Category>> GetUsedCategoriesByResourceTypeAsync(string resourceType, int? classId = null)
        {
            IQueryable<Category> query = _context.Categories;

            query = resourceType.ToLower() switch
            {
                "document" => query.Where(c => _context.Documents.Any(d => d.category_id == c.category_id)),
                "game" => query.Where(c => _context.Games.Any(g => g.category_id == c.category_id)),
                "video" => query.Where(c => _context.Videos.Any(v => v.category_id == c.category_id)),
                "comic" => query.Where(c => _context.Comics.Any(v => v.Category_id == c.category_id)),
                _ => throw new ArgumentException("Invalid resource type")
            };

            // Thêm điều kiện lọc theo classId nếu có
            if (classId.HasValue)
            {
                query = query.Where(c => c.class_id == classId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<CategorySimpleDto>> GetUsedCategoriesSimpleAsync(int? classId = null)
        {
            var usedCategoryIds = await _context.Documents.Select(d => d.category_id)
                .Union(_context.Games.Select(g => g.category_id))
                .Union(_context.Videos.Select(v => v.category_id))
                .Union(_context.Comics.Select(c => c.Category_id))
                .Distinct()
                .ToListAsync();

            var query = _context.Categories
                .Where(c => usedCategoryIds.Contains(c.category_id));

            // Thêm điều kiện lọc theo classId nếu có
            if (classId.HasValue)
            {
                query = query.Where(c => c.class_id == classId.Value);
            }

            return await query
                .Select(c => new CategorySimpleDto
                {
                    Id = c.category_id,
                    Name = c.name
                })
                .ToListAsync();
        }
    }
}
