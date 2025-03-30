using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LifeController : ControllerBase
    {
        private readonly ILifeService _lifeService;

        public LifeController(ILifeService lifeService)
        {
            _lifeService = lifeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LifeDto>> GetAllLifes()
        {
            var lives = _lifeService.GetAllLifes();
            return Ok(lives);
        }

        [HttpGet("{id}")]
        public ActionResult<LifeDto> GetLifeById(int id)
        {
            var life = _lifeService.GetLifeById(id);
            if (life == null)
            {
                return NotFound();
            }
            return Ok(life);
        }

        [HttpPost]
        public ActionResult AddLife([FromBody] CreateUpdateLifeDto lifeDto)
        {
            var existingLife = _lifeService.GetAllLifes()
                .FirstOrDefault(l => l.Question == lifeDto.Question && l.Category_id == lifeDto.Category_id);

            if (existingLife != null)
            {
                return BadRequest(new { message = "Đã có câu hỏi tương tự trong danh mục này!" });
            }

            var life = new Life
            {
                Question = lifeDto.Question,
                Answer = lifeDto.Answer,
                Category_id = lifeDto.Category_id,
                Uploaded_by = lifeDto.Uploaded_by,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _lifeService.AddLife(life);
                return CreatedAtAction(nameof(GetLifeById), new { id = life.Id }, life);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLife(int id, [FromBody] CreateUpdateLifeDto lifeDto)
        {
            var existingLife = _lifeService.GetAllLifes()
                .FirstOrDefault(l => l.Question == lifeDto.Question && l.Category_id == lifeDto.Category_id && l.Id != id);

            if (existingLife != null)
            {
                return BadRequest(new { message = "Đã có câu hỏi tương tự trong danh mục này!" });
            }

            var life = new Life
            {
                Id = id,
                Question = lifeDto.Question,
                Answer = lifeDto.Answer,
                Category_id = lifeDto.Category_id,
                Uploaded_by = lifeDto.Uploaded_by,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _lifeService.UpdateLife(life);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLife(int id)
        {
            _lifeService.DeleteLife(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchLives(
    [FromQuery] string? question,
    [FromQuery] int? categoryId,
    [FromQuery] int? classId)
        {
            try
            {
                var lives = await _lifeService.SearchLivesAsync(question, categoryId, classId);
                return Ok(lives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}