using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;


public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllGamesAsync();
        return games.Select(g => new GameDto
        {
            Id = g.game_id,
            title = g.title,
            description = g.description,
            gameUrl = g.game_url,
            category_id = g.category_id,
            category_name = g.Category?.name,
            uploaded_by = g.uploaded_by,
            UploadedByUsername = g.User?.username,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt
        }).ToList();
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null)
        {
            return null;
        }

        return new GameDto
        {
            Id = game.game_id,
            title = game.title,
            description = game.description,
            gameUrl = game.game_url,
            category_id = game.category_id,
            category_name = game.Category?.name,
            uploaded_by = game.uploaded_by,
            UploadedByUsername = game.User?.username,
            CreatedAt = game.CreatedAt,
            UpdatedAt = game.UpdatedAt
        };
    }

    public async Task AddGameAsync(Game game)
    {

        await _gameRepository.AddGameAsync(game);
    }

    public async Task UpdateGameAsync(Game game)
    {
        var existingGame = await _gameRepository.GetGameByIdAsync(game.game_id);
        if (existingGame == null)
        {
            throw new Exception("Game không tồn tại!");
        }

        var duplicateGame = (await _gameRepository.SearchGamesAsync(game.title, game.category_id, null))
            .FirstOrDefault(g => g.game_id != game.game_id);
        if (duplicateGame != null)
        {
            throw new Exception("Đã có game có tiêu đề tương tự trong danh mục này!");
        }

        // Cập nhật tất cả các trường
        existingGame.title = game.title;
        existingGame.description = game.description;
        existingGame.game_url = game.game_url;
        existingGame.category_id = game.category_id;
        existingGame.uploaded_by = game.uploaded_by;
        existingGame.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

        await _gameRepository.UpdateGameAsync(existingGame);
    }



    public async Task DeleteGameAsync(int id)
    {
        await _gameRepository.DeleteGameAsync(id);
    }
    public async Task<IEnumerable<GameDto>> SearchGamesAsync(string? name, int? categoryId, int? classId)
    {
        var videos = await _gameRepository.SearchGamesAsync(name, categoryId, classId);
        return videos.Select(g => new GameDto
        {
            Id = g.game_id,
            title = g.title,
            description = g.description,
            gameUrl = g.game_url,
            category_id = g.category_id,
            category_name = g.Category?.name,
            uploaded_by = g.uploaded_by,
            UploadedByUsername = g.User?.username,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt
        });
    }
}
