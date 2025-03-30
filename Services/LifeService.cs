using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class LifeService : ILifeService
    {
        private readonly ILifeRepository _lifeRepository;

        public LifeService(ILifeRepository lifeRepository)
        {
            _lifeRepository = lifeRepository;
        }

        public IEnumerable<LifeDto> GetAllLifes()
        {
            var lifes = _lifeRepository.GetAllLifes();
            return lifes.Select(l => new LifeDto
            {
                Id = l.Id,
                Question = l.Question,
                Answer = l.Answer,
                Category_id = l.Category_id,
                CategoryName = l.Category?.name,
                Uploaded_by = l.Uploaded_by,
                Username = l.User?.username,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            });
        }

        public LifeDto GetLifeById(int id)
        {
            var life = _lifeRepository.GetLifeById(id);
            if (life == null) return null;

            return new LifeDto
            {
                Id = life.Id,
                Question = life.Question,
                Answer = life.Answer,
                Category_id = life.Category_id,
                CategoryName = life.Category?.name,
                Uploaded_by = life.Uploaded_by,
                Username = life.User?.username,
                CreatedAt = life.CreatedAt,
                UpdatedAt = life.UpdatedAt
            };
        }

        public void AddLife(Life life)
        {
            _lifeRepository.AddLife(life);
        }

        public void UpdateLife(Life life)
        {
            _lifeRepository.UpdateLife(life);
        }

        public void DeleteLife(int id)
        {
            _lifeRepository.DeleteLife(id);
        }

        public async Task<IEnumerable<LifeDto>> SearchLivesAsync(string? question, int? categoryId, int? classId)
        {
            var lives = await _lifeRepository.SearchLifesAsync(question, categoryId, classId);
            return lives.Select(l => new LifeDto
            {
                Id = l.Id,
                Question = l.Question,
                Answer = l.Answer,
                Category_id = l.Category_id,
                CategoryName = l.Category?.name,
                Uploaded_by = l.Uploaded_by,
                Username = l.User?.username,
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            });
        }
    }
}
