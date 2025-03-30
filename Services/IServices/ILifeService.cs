using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface ILifeService
    {
        IEnumerable<LifeDto> GetAllLifes();
        LifeDto GetLifeById(int id);
        void AddLife(Life life);
        void UpdateLife(Life life);
        void DeleteLife(int id);
        Task<IEnumerable<LifeDto>> SearchLivesAsync(string? question, int? categoryId, int? classId);
    }
}