// ComicController.cs
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
    public class ComicController : ControllerBase
    {
        private readonly IComicService _comicService;

        public ComicController(IComicService comicService)
        {
            _comicService = comicService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComicDto>> GetAllComics()
        {
            var comics = _comicService.GetAllComics();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        public ActionResult<ComicDto> GetComicById(int id)
        {
            var comic = _comicService.GetComicById(id);
            if (comic == null)
            {
                return NotFound();
            }
            return Ok(comic);
        }

        [HttpPost]
        public ActionResult AddComic([FromBody] CreateUpdateComicDto comicDto)
        {
            var existingComic = _comicService.GetAllComics()
                .FirstOrDefault(c => c.Title == comicDto.Title && c.Category_id == comicDto.Category_id);

            if (existingComic != null)
            {
                return BadRequest(new { message = "Đã có truyện tranh cùng tên trong danh mục này!" });
            }

            var comic = new Comic
            {
                Title = comicDto.Title,
                Description = comicDto.Description,
                Comic_url = comicDto.Comic_url,
                Category_id = comicDto.Category_id,
                Uploaded_by = comicDto.Uploaded_by,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _comicService.AddComic(comic);
                return CreatedAtAction(nameof(GetComicById), new { id = comic.Id }, comic);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateComic(int id, [FromBody] CreateUpdateComicDto comicDto)
        {
            var existingComic = _comicService.GetAllComics()
                .FirstOrDefault(c => c.Title == comicDto.Title && c.Category_id == comicDto.Category_id && c.Id != id);

            if (existingComic != null)
            {
                return BadRequest(new { message = "Đã có truyện tranh cùng tên trong danh mục này!" });
            }

            var comic = new Comic
            {
                Id = id,
                Title = comicDto.Title,
                Description = comicDto.Description,
                Comic_url = comicDto.Comic_url,
                Category_id = comicDto.Category_id,
                Uploaded_by = comicDto.Uploaded_by,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _comicService.UpdateComic(comic);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComic(int id)
        {
            _comicService.DeleteComic(id);
            return NoContent();
        }

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
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<ComicDto>> GetComicsByCategoryId(int categoryId)
        {
            try
            {
                var comics = _comicService.GetComicsByCategoryId(categoryId);
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
    }
}