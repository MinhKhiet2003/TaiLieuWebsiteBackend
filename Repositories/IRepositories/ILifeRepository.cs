using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface ILifeRepository
    {
        IEnumerable<Life> GetAllLifes();
        Life? GetLifeById(int id);
        void AddLife(Life life);
        void UpdateLife(Life life);
        void DeleteLife(int id);
        Task<IEnumerable<Life>> SearchLifesAsync(string? question, int? categoryId, int? classId);
    }
}