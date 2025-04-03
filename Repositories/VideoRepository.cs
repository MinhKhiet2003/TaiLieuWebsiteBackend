using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _context.Videos.Include(d => d.Category).Include(d => d.User).ToList();
        }

        public Video? GetVideoById(int id)
        {
            return _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .FirstOrDefault(v => v.video_id == id);
        }

        public void AddVideo(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        public void UpdateVideo(Video video)
        {
            var existingVideo = _context.Videos.Local.FirstOrDefault(v => v.video_id == video.video_id);
            if (existingVideo != null)
            {
                _context.Entry(existingVideo).State = EntityState.Detached;
            }
            _context.Videos.Update(video);
            _context.SaveChanges();
        }

        public void DeleteVideo(int id)
        {
            var video = _context.Videos.Find(id);
            if (video != null)
            {
                _context.Videos.Remove(video);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Video>> SearchVideosAsync(string? name, int? categoryId, int? classId)
        {
            var query = _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(v => v.title.Contains(name));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(v => v.category_id == categoryId);
            }

            if (classId.HasValue)
            {
                query = query.Where(v => v.Category.class_id == classId);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<VideoDto>> GetVideosByCategoryIdAsync(int categoryId)
        {
            return await Task.FromResult(_context.Videos.Where(v => v.category_id == categoryId).Select(v => new VideoDto
            {
                video_id = v.video_id,
                title = v.title,
                description = v.description,
                video_url = v.video_url,
                category_id = v.category_id,
                uploaded_by = v.uploaded_by,
                created_at = v.CreatedAt,
                updated_at = v.UpdatedAt
            }).ToList());
        }
    }
}
