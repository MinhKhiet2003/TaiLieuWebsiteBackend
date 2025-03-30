
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

        public IEnumerable<Comic> GetAllComics()
        {
            return _context.Comics.Include(c => c.Category).Include(c => c.User).ToList();
        }

        public Comic? GetComicById(int id)
        {
            return _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == id);
        }

        public void AddComic(Comic comic)
        {
            _context.Comics.Add(comic);
            _context.SaveChanges();
        }

        public void UpdateComic(Comic comic)
        {
            var existingComic = _context.Comics.Find(comic.Id);
            if (existingComic != null)
            {
                existingComic.Title = comic.Title;
                existingComic.Description = comic.Description;
                existingComic.Comic_url = comic.Comic_url;
                existingComic.Category_id = comic.Category_id;
                existingComic.Uploaded_by = comic.Uploaded_by;
                existingComic.UpdatedAt = comic.UpdatedAt;

                _context.SaveChanges();
            }
        }

        public void DeleteComic(int id)
        {
            var comic = _context.Comics.Find(id);
            if (comic != null)
            {
                _context.Comics.Remove(comic);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Comic>> SearchComicsAsync(string? title, int? categoryId, int? classId)
        {
            var query = _context.Comics
                .Include(c => c.Category)
                .Include(c => c.User)
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
    }
}