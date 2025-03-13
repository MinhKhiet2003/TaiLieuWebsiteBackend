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
            Title = g.title,
            Description = g.description,
            GameUrl = g.game_url,
            CategoryId = g.category_id,
            UploadedBy = g.uploaded_by,
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
            Title = game.title,
            Description = game.description,
            GameUrl = game.game_url,
            CategoryId = game.category_id,
            UploadedBy = game.uploaded_by,
            CreatedAt = game.CreatedAt,
            UpdatedAt = game.UpdatedAt
        };
    }

    public async Task<GameDto> AddGameAsync(GameDto gameDto)
    {
        var game = new Game
        {
            title = gameDto.Title,
            description = gameDto.Description,
            game_url = gameDto.GameUrl,
            category_id = gameDto.CategoryId,
            uploaded_by = gameDto.UploadedBy,
            CreatedAt = gameDto.CreatedAt,
            UpdatedAt = gameDto.UpdatedAt
        };

        var addedGame = await _gameRepository.AddGameAsync(game);
        gameDto.Id = addedGame.game_id;
        return gameDto;
    }

    public async Task<GameDto> UpdateGameAsync(GameDto gameDto)
    {
        var game = await _gameRepository.GetGameByIdAsync(gameDto.Id);
        if (game == null)
        {
            return null;
        }

        game.title = gameDto.Title;
        game.description = gameDto.Description;
        game.game_url = gameDto.GameUrl;
        game.category_id = gameDto.CategoryId;
        game.uploaded_by = gameDto.UploadedBy;
        game.CreatedAt = gameDto.CreatedAt;
        game.UpdatedAt = gameDto.UpdatedAt;

        await _gameRepository.UpdateGameAsync(game);
        return gameDto;
    }

    public async Task DeleteGameAsync(int id)
    {
        await _gameRepository.DeleteGameAsync(id);
    }
}
