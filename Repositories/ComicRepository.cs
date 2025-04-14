using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class ComicRepository : IComicRepository
    {
        private readonly AppDbContext _context;

        public ComicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comic>> GetAllComicsAsync()
        {
            return await _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
                .OrderByDescending(c => c.UpdatedAt).ThenByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<Comic?> GetComicByIdAsync(int id)
        {
            return await _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddComicAsync(Comic comic)
        {
            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComicAsync(Comic comic)
        {
            _context.Entry(comic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComicAsync(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic != null)
            {
                _context.Comics.Remove(comic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comic>> SearchComicsAsync(string? title, int? categoryId, int? classId)
        {
            var query = _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
                .OrderByDescending(c => c.UpdatedAt).ThenByDescending(c => c.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.Category_id == categoryId);
            }

            if (classId.HasValue)
            {
                query = query.Where(c => c.Category.class_id == classId);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Comic>> GetComicsByCategoryIdAsync(int categoryId)
        {
            return await _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
                .Where(c => c.Category_id == categoryId)
                .ToListAsync();
        }
    }
}