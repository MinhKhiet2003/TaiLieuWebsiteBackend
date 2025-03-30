using System.Collections.Generic;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services
{
    public interface IVideoService
    {
        IEnumerable<VideoDto> GetAllVideos();
        VideoDto GetVideoById(int id);
        void AddVideo(Video video);
        void UpdateVideo(Video video);
        void DeleteVideo(int id);
        Task<IEnumerable<VideoDto>> SearchVideosAsync(string? name, int? categoryId, int? classId);
    }
}
