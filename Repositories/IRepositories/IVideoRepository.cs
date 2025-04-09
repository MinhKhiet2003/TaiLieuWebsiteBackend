using System.Collections.Generic;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetAllVideos();
        Video GetVideoById(int id);
        void AddVideo(Video video);
        void UpdateVideo(Video video);
        void DeleteVideo(int id);
        Task<IEnumerable<Video>> SearchVideosAsync(string? name, int? categoryId, int? classId);
        Task<IEnumerable<VideoDto>> GetVideosByCategoryIdAsync(int categoryId);
    }
}
