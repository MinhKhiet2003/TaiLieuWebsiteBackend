using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class LifeRepository : ILifeRepository
    {
        private readonly AppDbContext _context;

        public LifeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Life> GetAllLifes()
        {
            return _context.Lifes.Include(l => l.Category).Include(l => l.User).ToList();
        }

        public Life? GetLifeById(int id)
        {
            return _context.Lifes
                .Include(l => l.Category)
                .Include(l => l.User)
                .FirstOrDefault(l => l.Id == id);
        }

        public void AddLife(Life life)
        {
            _context.Lifes.Add(life);
            _context.SaveChanges();
        }

        public void UpdateLife(Life life)
        {
            var existingLife = _context.Lifes.Find(life.Id);
            if (existingLife == null)
            {
                throw new ArgumentException($"Life with ID {life.Id} not found");
            }

            existingLife.Question = life.Question;
            existingLife.Answer = life.Answer;
            existingLife.Category_id = life.Category_id;
            existingLife.Uploaded_by = life.Uploaded_by;
            existingLife.UpdatedAt = DateTime.Now; 

            _context.SaveChanges();
        }

        public void DeleteLife(int id)
        {
            var life = _context.Lifes.Find(id);
            if (life != null)
            {
                _context.Lifes.Remove(life);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Life>> SearchLifesAsync(string? question, int? categoryId, int? classId)
        {
            var query = _context.Lifes
                .Include(l => l.Category)
                .Include(l => l.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(question))
            {
                query = query.Where(l => l.Question.Contains(question));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(l => l.Category_id == categoryId);
            }

            if (classId.HasValue)
            {
                query = query.Where(l => l.Category.class_id == classId);
            }

            return await query.ToListAsync();
        }
    }
}
