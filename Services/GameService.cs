using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;


public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IStarRepository _starRepository;
    public GameService(IGameRepository gameRepository, ICommentRepository commentRepository, IStarRepository starRepository)
    {
        _gameRepository = gameRepository;
        _commentRepository = commentRepository;
        _starRepository = starRepository;
    }

    public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllGamesAsync();
        var gameIds = games.Select(g => g.game_id).ToList();
        var commentCounts = _commentRepository.GetCommentCountsByGameIds(gameIds);
        var averageRatings = _starRepository.GetAverageRatingsByGameIds(gameIds);

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
            classify = g.classify,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt,
            CommentCount = commentCounts.TryGetValue(g.game_id, out int count) ? count : 0,
            AverageRating = averageRatings.TryGetValue(g.game_id, out double rating) ? rating : 0
        }).ToList();
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        if (game == null)
        {
            return null;
        }

        var commentCount = await _commentRepository.CountByGameIdAsync(id);
        var averageRating = await _starRepository.GetAverageByGameIdAsync(id);

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
            classify = game.classify,
            CreatedAt = game.CreatedAt,
            UpdatedAt = game.UpdatedAt,
            CommentCount = commentCount,
            AverageRating = averageRating
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

        var duplicateGame = (await _gameRepository.SearchGamesAsync(game.title, game.category_id, null,game.classify))
            .FirstOrDefault(g => g.game_id != game.game_id);
        if (duplicateGame != null)
        {
            throw new Exception("Đã có game có tiêu đề tương tự !");
        }

        // Cập nhật tất cả các trường
        existingGame.title = game.title;
        existingGame.description = game.description;
        existingGame.game_url = game.game_url;
        existingGame.category_id = game.category_id;
        existingGame.uploaded_by = game.uploaded_by;
        existingGame.classify = game.classify;
        existingGame.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

        await _gameRepository.UpdateGameAsync(existingGame);
    }



    public async Task DeleteGameAsync(int id)
    {
        await _gameRepository.DeleteGameAsync(id);
    }
    public async Task<IEnumerable<GameDto>> SearchGamesAsync(string? name, int? categoryId, int? classId, string? classify)
    {
        var games = await _gameRepository.SearchGamesAsync(name, categoryId, classId, classify);
        var gameIds = games.Select(g => g.game_id).ToList();
        var commentCounts = _commentRepository.GetCommentCountsByGameIds(gameIds);
        var averageRatings = _starRepository.GetAverageRatingsByGameIds(gameIds);

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
            classify = g.classify,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt,
            CommentCount = commentCounts.TryGetValue(g.game_id, out int count) ? count : 0,
            AverageRating = averageRatings.TryGetValue(g.game_id, out double rating) ? rating : 0
        });
    }
    public async Task<IEnumerable<GameDto>> GetGamesByCategoryIdAsync(int categoryId)
    {
        var games = await _gameRepository.GetGamesByCategoryIdAsync(categoryId);
        var gameIds = games.Select(g => g.game_id).ToList();
        var commentCounts = _commentRepository.GetCommentCountsByGameIds(gameIds);
        var averageRatings = _starRepository.GetAverageRatingsByGameIds(gameIds);

        return games.Select(g => new GameDto
        {
            Id = g.game_id,
            title = g.title,
            description = g.description,
            gameUrl = g.game_url,
            category_id = g.category_id,
            uploaded_by = g.uploaded_by,
            classify = g.classify,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt,
            CommentCount = commentCounts.TryGetValue(g.game_id, out int count) ? count : 0,
            AverageRating = averageRatings.TryGetValue(g.game_id, out double rating) ? rating : 0
        }).ToList();
    }
    public async Task<IEnumerable<int>> GetUsedCategoryIdsAsync()
    {
        var games = await _gameRepository.GetAllGamesAsync();
        return games.Select(g => g.category_id).Distinct();
    }
}
