using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IComicService
    {
        Task<IEnumerable<ComicDto>> GetAllComicsAsync();
        Task<ComicDto> GetComicByIdAsync(int id);
        Task AddComicAsync(Comic comic);
        Task UpdateComicAsync(Comic comic);
        Task DeleteComicAsync(int id);
        Task<IEnumerable<ComicDto>> SearchComicsAsync(string? title, int? categoryId, int? classId);
        Task<IEnumerable<int>> GetUsedCategoryIdsAsync();
        Task<IEnumerable<ComicDto>> GetComicsByCategoryIdAsync(int categoryId);
    }
}