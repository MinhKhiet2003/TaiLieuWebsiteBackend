using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<GameDto> GetGameByIdAsync(int id);
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
        Task<IEnumerable<GameDto>> SearchGamesAsync(string? name, int? categoryId, int? classId);
        Task<IEnumerable<GameDto>> GetGamesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<int>> GetUsedCategoryIdsAsync();
    }
}
