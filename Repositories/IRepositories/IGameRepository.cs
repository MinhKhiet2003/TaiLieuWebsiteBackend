using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<Game> AddGameAsync(Game game);
        Task<Game> UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
    }
}
