using Microsoft.AspNetCore.Mvc;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Services;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateDto dto)
        {
            try
            {
                var comment = await _commentService.CreateCommentAsync(
                    dto.Content,
                    dto.UserId,
                    dto.DocumentId,
                    dto.GameId,
                    dto.VideoId,
                    dto.ComicId
                );
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update(int commentId, [FromBody] CommentUpdateDto dto)
        {
            try
            {
                var comment = await _commentService.UpdateCommentAsync(commentId, dto.Content, dto.UserId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId, [FromQuery] int userId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] int? documentId = null,
            [FromQuery] int? gameId = null, [FromQuery] int? videoId = null,
            [FromQuery] int? comicId = null)
        {
            try
            {
                var comments = await _commentService.GetCommentsByIdAsync(documentId, gameId, videoId, comicId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountComments([FromQuery] int? documentId = null,
            [FromQuery] int? gameId = null, [FromQuery] int? videoId = null,
            [FromQuery] int? comicId = null)
        {
            try
            {
                var count = await _commentService.CountCommentsAsync(documentId, gameId, videoId, comicId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}