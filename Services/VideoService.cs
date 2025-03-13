using System.Collections.Generic;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories;
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

        public IEnumerable<Video> GetAllVideos()
        {
            return _videoRepository.GetAllVideos();
        }

        public Video GetVideoById(int id)
        {
            return _videoRepository.GetVideoById(id);
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
    }
}
