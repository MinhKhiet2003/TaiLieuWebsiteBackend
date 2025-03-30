using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services;
using System.Linq;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VideoController(IVideoService videoService, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _videoService = videoService;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VideoDto>> GetAllVideos()
        {
            var videos = _videoService.GetAllVideos();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public ActionResult<VideoDto> GetVideoById(int id)
        {
            var video = _videoService.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        public ActionResult AddVideo([FromBody] CreateUpdateVideoDto videoDto)
        {
            var existingVideo = _videoService.GetAllVideos()
                .FirstOrDefault(v => v.title == videoDto.title && v.category_id == videoDto.category_id);

            if (existingVideo != null)
            {
                return BadRequest(new { message = "Đã có video có tiêu đề tương tự trong danh mục này!" });
            }

            var video = new Video
            {
                title = videoDto.title,
                description = videoDto.description,
                video_url = videoDto.video_url,
                category_id = videoDto.category_id,
                uploaded_by = videoDto.uploaded_by,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _videoService.AddVideo(video);
                return CreatedAtAction(nameof(GetVideoById), new { id = video.video_id }, video);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateVideo(int id, [FromBody] CreateUpdateVideoDto videoDto)
        {
            var existingVideo = _videoService.GetAllVideos()
                .FirstOrDefault(v => v.title == videoDto.title && v.category_id == videoDto.category_id && v.video_id != id);

            if (existingVideo != null)
            {
                return BadRequest(new { message = "Đã có video có tiêu đề tương tự trong danh mục này!" });
            }

            var video = new Video
            {
                video_id = id,
                title = videoDto.title,
                description = videoDto.description,
                video_url = videoDto.video_url,
                category_id = videoDto.category_id,
                uploaded_by = videoDto.uploaded_by,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _videoService.UpdateVideo(video);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVideo(int id)
        {
            _videoService.DeleteVideo(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVideos(
            [FromQuery] string? name,
            [FromQuery] int? categoryId,
            [FromQuery] int? classId)
        {
            var videos = await _videoService.SearchVideosAsync(name, categoryId, classId);
            return Ok(videos);
        }
    }
}
