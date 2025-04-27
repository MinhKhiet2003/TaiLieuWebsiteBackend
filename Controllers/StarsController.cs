using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        private readonly IStarService _starService;

        public StarController(IStarService starService)
        {
            _starService = starService;
        }
        [HttpPost("user")]
        public async Task<IActionResult> CreateOrUpdate(
                    [FromBody] StarCreateDto dto,
                    [FromQuery] int userId,
                    [FromQuery] int? documentId = null,
                    [FromQuery] int? exerciseId = null,
                    [FromQuery] int? gameId = null,
                    [FromQuery] int? videoId = null,
                    [FromQuery] int? comicId = null)
        {
            try
            {
                var star = await _starService.CreateOrUpdateRatingAsync(
                    dto.Rating,
                    userId,
                    documentId,
                    exerciseId,
                    gameId,
                    videoId,
                    comicId
                );
                return Ok(star);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("user")]
        public async Task<IActionResult> GetRatingByUserAndContent(
            [FromQuery] int userId,
            [FromQuery] int? documentId = null,
            [FromQuery] int? exerciseId = null,
            [FromQuery] int? gameId = null,
            [FromQuery] int? videoId = null,
            [FromQuery] int? comicId = null)
        {
            try
            {
                var rating = await _starService.GetRatingByUserAndContentAsync(
                    userId, documentId, exerciseId, gameId, videoId, comicId);
                if (rating == null)
                    return NotFound("No rating found for the specified user and content.");
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("averageByContentType")]
        public async Task<IActionResult> GetAverageRatingsByContentType([FromQuery] string contentType)
        {
            try
            {
                var averages = await _starService.GetAverageRatingsByContentTypeAsync(contentType);
                return Ok(averages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRatings([FromQuery] int? documentId = null,
            [FromQuery] int? exerciseId = null, [FromQuery] int? gameId = null,
            [FromQuery] int? videoId = null, [FromQuery] int? comicId = null)
        {
            try
            {
                var ratings = await _starService.GetRatingsByIdAsync(documentId, exerciseId, gameId, videoId, comicId);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("average")]
        public async Task<IActionResult> GetAverageRating([FromQuery] int? documentId = null,
            [FromQuery] int? exerciseId = null, [FromQuery] int? gameId = null,
            [FromQuery] int? videoId = null, [FromQuery] int? comicId = null)
        {
            try
            {
                var average = await _starService.GetAverageRatingAsync(documentId, exerciseId, gameId, videoId, comicId);
                return Ok(average);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}