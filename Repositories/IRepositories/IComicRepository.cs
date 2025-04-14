using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IComicRepository
    {
        Task<IEnumerable<Comic>> GetAllComicsAsync();
        Task<Comic?> GetComicByIdAsync(int id);
        Task AddComicAsync(Comic comic);
        Task UpdateComicAsync(Comic comic);
        Task DeleteComicAsync(int id);
        Task<IEnumerable<Comic>> SearchComicsAsync(string? title, int? categoryId, int? classId);
        Task<IEnumerable<Comic>> GetComicsByCategoryIdAsync(int categoryId);
    }
}