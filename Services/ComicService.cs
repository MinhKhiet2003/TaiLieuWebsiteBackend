using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class ComicService : IComicService
    {
        private readonly IComicRepository _comicRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IStarRepository _starRepository;
        public ComicService(IComicRepository comicRepository, ICommentRepository commentRepository, IStarRepository starRepository)
        {
            _comicRepository = comicRepository;
            _commentRepository = commentRepository;
            _starRepository = starRepository;
        }

        public async Task<IEnumerable<ComicDto>> GetAllComicsAsync()
        {
            var comics = await _comicRepository.GetAllComicsAsync();
            var comicIds = comics.Select(c => c.Id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByComicIds(comicIds);
            var averageRatings = _starRepository.GetAverageRatingsByComicIds(comicIds);

            return comics.Select(c => new ComicDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Comic_url = c.Comic_url,
                Category_id = c.Category_id,
                CategoryName = c.Category?.name,
                Uploaded_by = c.Uploaded_by,
                Username = c.User?.username,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                CommentCount = commentCounts.TryGetValue(c.Id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(c.Id, out double rating) ? rating : 0
            }).ToList();
        }

        public async Task<ComicDto> GetComicByIdAsync(int id)
        {
            var comic = await _comicRepository.GetComicByIdAsync(id);
            if (comic == null) return null;

            var commentCount = await _commentRepository.CountByComicIdAsync(id);
            var averageRating = await _starRepository.GetAverageByComicIdAsync(id);

            return new ComicDto
            {
                Id = comic.Id,
                Title = comic.Title,
                Description = comic.Description,
                Comic_url = comic.Comic_url,
                Category_id = comic.Category_id,
                CategoryName = comic.Category?.name,
                Uploaded_by = comic.Uploaded_by,
                Username = comic.User?.username,
                CreatedAt = comic.CreatedAt,
                UpdatedAt = comic.UpdatedAt,
                CommentCount = commentCount,
                AverageRating = averageRating
            };
        }

        public async Task AddComicAsync(Comic comic)
        {
            await _comicRepository.AddComicAsync(comic);
        }

        public async Task UpdateComicAsync(Comic comic)
        {
            var existingComic = await _comicRepository.GetComicByIdAsync(comic.Id);
            if (existingComic == null)
            {
                throw new Exception("Comic không tồn tại!");
            }

            var duplicateComic = (await _comicRepository.SearchComicsAsync(comic.Title, comic.Category_id, null))
                .FirstOrDefault(c => c.Id != comic.Id);
            if (duplicateComic != null)
            {
                throw new Exception("Đã có truyện tranh cùng tên trong danh mục này!");
            }

            existingComic.Title = comic.Title;
            existingComic.Description = comic.Description;
            existingComic.Comic_url = comic.Comic_url;
            existingComic.Category_id = comic.Category_id;
            existingComic.Uploaded_by = comic.Uploaded_by;
            existingComic.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            await _comicRepository.UpdateComicAsync(existingComic);
        }

        public async Task DeleteComicAsync(int id)
        {
            await _comicRepository.DeleteComicAsync(id);
        }

        public async Task<IEnumerable<ComicDto>> SearchComicsAsync(string? title, int? categoryId, int? classId)
        {
            var comics = await _comicRepository.SearchComicsAsync(title, categoryId, classId);
            var comicIds = comics.Select(c => c.Id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByComicIds(comicIds);
            var averageRatings = _starRepository.GetAverageRatingsByComicIds(comicIds);

            return comics.Select(c => new ComicDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Comic_url = c.Comic_url,
                Category_id = c.Category_id,
                CategoryName = c.Category?.name,
                Uploaded_by = c.Uploaded_by,
                Username = c.User?.username,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                CommentCount = commentCounts.TryGetValue(c.Id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(c.Id, out double rating) ? rating : 0
            });
        }

        public async Task<IEnumerable<int>> GetUsedCategoryIdsAsync()
        {
            var comics = await _comicRepository.GetAllComicsAsync();
            return comics.Select(c => c.Category_id).Distinct();
        }

        public async Task<IEnumerable<ComicDto>> GetComicsByCategoryIdAsync(int categoryId)
        {
            var comics = await _comicRepository.GetComicsByCategoryIdAsync(categoryId);
            var comicIds = comics.Select(c => c.Id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByComicIds(comicIds);
            var averageRatings = _starRepository.GetAverageRatingsByComicIds(comicIds);

            return comics.Select(c => new ComicDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Comic_url = c.Comic_url,
                Category_id = c.Category_id,
                CategoryName = c.Category?.name,
                Uploaded_by = c.Uploaded_by,
                Username = c.User?.username,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                CommentCount = commentCounts.TryGetValue(c.Id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(c.Id, out double rating) ? rating : 0
            }).ToList();
        }
    }
}