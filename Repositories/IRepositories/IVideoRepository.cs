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
    }
}
