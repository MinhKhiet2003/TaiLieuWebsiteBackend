using TaiLieuWebsiteBackend.DTOs;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<GameDto> GetGameByIdAsync(int id);
        Task<GameDto> AddGameAsync(GameDto gameDto);
        Task<GameDto> UpdateGameAsync(GameDto gameDto);
        Task DeleteGameAsync(int id);
    }
}
