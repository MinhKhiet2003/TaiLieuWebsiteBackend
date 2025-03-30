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

        public GameController(IGameService gameService, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _gameService = gameService;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
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

            var game = new Game
            {
                title = gameDto.title,
                description = gameDto.description,
                game_url = gameDto.gameUrl,
                category_id = gameDto.category_id,
                uploaded_by = gameDto.uploaded_by,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
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
                    uploaded_by = gameDto.uploaded_by
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
            [FromQuery] int? classId)
        {
            var games = await _gameService.SearchGamesAsync(name, categoryId, classId);
            return Ok(games);
        }
    }
}
