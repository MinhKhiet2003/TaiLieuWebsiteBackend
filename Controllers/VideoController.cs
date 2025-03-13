using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Video>> GetAllVideos()
        {
            return Ok(_videoService.GetAllVideos());
        }

        [HttpGet("{id}")]
        public ActionResult<VideoDTO> GetVideoById(int id)
        {
            var video = _videoService.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }

            var videoDTO = new VideoDTO
            {
                video_id = video.video_id,
                video_url = video.video_url,
                title = video.title,
                description = video.description,
                category_id = video.category_id,
                uploaded_by = video.uploaded_by,
                created_at = video.CreatedAt,
                updated_at = video.UpdatedAt
            };

            return Ok(videoDTO);
        }


        [HttpPost]
        public ActionResult AddVideo([FromBody] VideoDTO videoDTO)
        {
            var video = new Video
            {
                video_url = videoDTO.video_url,
                title = videoDTO.title,
                category_id = videoDTO.category_id,
                uploaded_by = videoDTO.uploaded_by,
                CreatedAt = videoDTO.created_at,
                UpdatedAt = videoDTO.created_at,
                description = videoDTO.description ?? ""
            };

            _videoService.AddVideo(video);
            return CreatedAtAction(nameof(GetVideoById), new { id = video.video_id }, video);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateVideo(int id, [FromBody] VideoDTO videoDTO)
        {
            if (id != videoDTO.video_id)
            {
                return BadRequest();
            }

            var video = new Video
            {
                video_id = videoDTO.video_id,
                video_url = videoDTO.video_url,
                title = videoDTO.title,
                category_id = videoDTO.category_id,
                uploaded_by = videoDTO.uploaded_by,
                CreatedAt = videoDTO.created_at,
                UpdatedAt = videoDTO.updated_at,
                description = videoDTO.description ?? ""
            };

            _videoService.UpdateVideo(video);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVideo(int id)
        {
            _videoService.DeleteVideo(id);
            return NoContent();
        }
    }
}
