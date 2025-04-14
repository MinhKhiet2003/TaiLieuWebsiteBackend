using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.DTOs;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;

        public GameController(
            IGameService gameService,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICommentRepository commentRepository)
        {
            _gameService = gameService;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] CreateUpdateGameDto gameDto)
        {
            var games = await _gameService.GetAllGamesAsync();
            var existingGame = games.FirstOrDefault(g => g.title == gameDto.title && g.category_id == gameDto.category_id);

            if (existingGame != null)
            {
                return BadRequest(new { message = "Đã có game có tiêu đề tương tự trong danh mục này!" });
            }
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); 
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            var game = new Game
            {
                title = gameDto.title,
                description = gameDto.description,
                game_url = gameDto.gameUrl,
                category_id = gameDto.category_id,
                uploaded_by = gameDto.uploaded_by,
                classify = gameDto.classify,
                CreatedAt = currentTime,
                UpdatedAt = currentTime
            };

            try
            {
                await _gameService.AddGameAsync(game);
                return CreatedAtAction(nameof(GetGameById), new { id = game.game_id }, game);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(int id, [FromBody] CreateUpdateGameDto gameDto)
        {
            try
            {
                var game = new Game
                {
                    game_id = id,
                    title = gameDto.title,
                    description = gameDto.description,
                    game_url = gameDto.gameUrl,
                    category_id = gameDto.category_id,
                    uploaded_by = gameDto.uploaded_by,
                    classify = gameDto.classify
                };

                await _gameService.UpdateGameAsync(game);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            _gameService.DeleteGameAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGames(
            [FromQuery] string? name,
            [FromQuery] int? categoryId,
            [FromQuery] int? classId,
            [FromQuery] string? classify)
        {
            var games = await _gameService.SearchGamesAsync(name, categoryId, classId, classify);
            return Ok(games);
        }
        [AllowAnonymous]
        [HttpGet("random")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetRandomGames()
        {
            var games = (await _gameService.GetAllGamesAsync())
                        .OrderBy(g => Guid.NewGuid()) 
                        .Take(3) 
                        .Select(g => new GameDto
                        {
                            Id = g.Id,
                            title = g.title,
                            description = g.description,
                            gameUrl = g.gameUrl,
                            category_id = g.category_id,
                            uploaded_by = g.uploaded_by,
                            UploadedByUsername = g.UploadedByUsername ?? "Không xác định",
                            classify = g.classify,
                            CreatedAt = g.CreatedAt,
                            UpdatedAt = g.UpdatedAt,
                            CommentCount = g.CommentCount,
                            AverageRating = g.AverageRating,
                        })
                        .ToList();

            return Ok(games);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByCategoryId(int categoryId)
        {
            try
            {
                var games = await _gameService.GetGamesByCategoryIdAsync(categoryId);
                if (games == null || !games.Any())
                {
                    return NotFound("No games found for the given category ID.");
                }
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching games.", error = ex.Message });
            }
        }
    }
}
