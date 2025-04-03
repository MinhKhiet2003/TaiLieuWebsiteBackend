using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task AddGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
        Task<IEnumerable<Game>> SearchGamesAsync(string? name, int? categoryId, int? classId);
        Task<IEnumerable<Game>> GetGamesByCategoryIdAsync(int categoryId);
    }
}
