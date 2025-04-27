using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly IComicService _comicService;
        private readonly ICommentRepository _commentRepository; 

        public ComicController(IComicService comicService, ICommentRepository commentRepository)
        {
            _comicService = comicService;
            _commentRepository = commentRepository; // Khởi tạo
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicDto>>> GetAllComics()
        {
            var comics = await _comicService.GetAllComicsAsync(); // Chuyển sang async
            return Ok(comics);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicDto>> GetComicById(int id)
        {
            var comic = await _comicService.GetComicByIdAsync(id); // Chuyển sang async
            if (comic == null)
            {
                return NotFound();
            }
            return Ok(comic);
        }

        [HttpPost]
        public async Task<ActionResult> AddComic([FromBody] CreateUpdateComicDto comicDto)
        {
            var comics = await _comicService.GetAllComicsAsync();
            var existingComic = comics.FirstOrDefault(c => c.Title == comicDto.Title && c.Category_id == comicDto.Category_id);

            if (existingComic != null)
            {
                return BadRequest(new { message = "Đã có truyện tranh cùng tên trong danh mục này!" });
            }

            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            var comic = new Comic
            {
                Title = comicDto.Title,
                Description = comicDto.Description,
                Comic_url = comicDto.Comic_url,
                Category_id = comicDto.Category_id,
                Uploaded_by = comicDto.Uploaded_by,
                CreatedAt = currentTime,
                UpdatedAt = currentTime
            };

            try
            {
                await _comicService.AddComicAsync(comic); // Chuyển sang async
                return CreatedAtAction(nameof(GetComicById), new { id = comic.Id }, comic);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComic(int id, [FromBody] CreateUpdateComicDto comicDto)
        {
            try
            {
                var comic = new Comic
                {
                    Id = id,
                    Title = comicDto.Title,
                    Description = comicDto.Description,
                    Comic_url = comicDto.Comic_url,
                    Category_id = comicDto.Category_id,
                    Uploaded_by = comicDto.Uploaded_by,
                    UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))
                };

                await _comicService.UpdateComicAsync(comic); // Chuyển sang async
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComic(int id)
        {
            await _comicService.DeleteComicAsync(id); // Chuyển sang async
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchComics(
            [FromQuery] string? title,
            [FromQuery] int? categoryId,
            [FromQuery] int? classId)
        {
            try
            {
                var comics = await _comicService.SearchComicsAsync(title, categoryId, classId);
                return Ok(comics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ComicDto>>> GetComicsByCategoryId(int categoryId)
        {
            try
            {
                var comics = await _comicService.GetComicsByCategoryIdAsync(categoryId); // Chuyển sang async
                if (comics == null || !comics.Any())
                {
                    return NotFound("No comics found for the given category ID.");
                }
                return Ok(comics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching comics.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        // Thêm endpoint để lấy danh sách comment theo comicId
        [HttpGet("{comicId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByComicId(int comicId)
        {
            var comments = await _commentRepository.GetByComicIdAsync(comicId);
            return Ok(comments);
        }

        [AllowAnonymous]
        // Thêm endpoint để đếm số lượng comment theo comicId
        [HttpGet("{comicId}/comments/count")]
        public async Task<ActionResult<int>> GetCommentCountByComicId(int comicId)
        {
            var count = await _commentRepository.CountByComicIdAsync(comicId);
            return Ok(count);
        }
    }
}