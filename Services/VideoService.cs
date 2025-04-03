using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IEnumerable<VideoDto> GetAllVideos()
        {
            var videos = _videoRepository.GetAllVideos();
            return videos.Select(v => new VideoDto
            {
                video_id = v.video_id,
                video_url = v.video_url,
                title = v.title,
                description = v.description,
                category_id = v.category_id,
                category_name = v.Category.name,
                uploaded_by = v.uploaded_by,
                UploadedByUsername = v.User?.username,
                created_at = v.CreatedAt,
                updated_at = v.UpdatedAt
            });
        }

        public VideoDto GetVideoById(int id)
        {
            var video = _videoRepository.GetVideoById(id);
            if (video == null) return null;

            return new VideoDto
            {
                video_id = video.video_id,
                video_url = video.video_url,
                title = video.title,
                description = video.description,
                category_id = video.category_id,
                category_name = video.Category.name,
                uploaded_by = video.uploaded_by,
                UploadedByUsername = video.User?.username,
                created_at = video.CreatedAt,
                updated_at = video.UpdatedAt
            };
        }

        public void AddVideo(Video video)
        {
            _videoRepository.AddVideo(video);
        }

        public void UpdateVideo(Video video)
        {
            _videoRepository.UpdateVideo(video);
        }

        public void DeleteVideo(int id)
        {
            _videoRepository.DeleteVideo(id);
        }

        public async Task<IEnumerable<VideoDto>> SearchVideosAsync(string? name, int? categoryId, int? classId)
        {
            var videos = await _videoRepository.SearchVideosAsync(name, categoryId, classId);
            return videos.Select(v => new VideoDto
            {
                video_id = v.video_id,
                video_url = v.video_url,
                title = v.title,
                description = v.description,
                category_id = v.category_id,
                category_name = v.Category?.name,
                uploaded_by = v.uploaded_by,
                UploadedByUsername = v.User?.username,
                created_at = v.CreatedAt,
                updated_at = v.UpdatedAt
            });
        }
        public async Task<IEnumerable<VideoDto>> GetVideosByCategoryIdAsync(int categoryId)
        {
            return await _videoRepository.GetVideosByCategoryIdAsync(categoryId);
        }
        public async Task<IEnumerable<int>> GetUsedCategoryIdsAsync()
        {
            var videos = _videoRepository.GetAllVideos();
            return videos.Select(v => v.category_id).Distinct();
        }
    }
}
