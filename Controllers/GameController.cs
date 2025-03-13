using Microsoft.AspNetCore.Mvc;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Services.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame([FromBody] GameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newGame = await _gameService.AddGameAsync(gameDto);
            return CreatedAtAction(nameof(GetGameById), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameDto gameDto)
        {
            if (id != gameDto.Id)
            {
                return BadRequest();
            }
            var updatedGame = await _gameService.UpdateGameAsync(gameDto);
            if (updatedGame == null)
            {
                return NotFound();
            }
            return Ok(updatedGame);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }
    }
}

