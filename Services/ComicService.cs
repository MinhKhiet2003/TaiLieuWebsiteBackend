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

        public ComicService(IComicRepository comicRepository)
        {
            _comicRepository = comicRepository;
        }

        public IEnumerable<ComicDto> GetAllComics()
        {
            var comics = _comicRepository.GetAllComics();
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
                UpdatedAt = c.UpdatedAt
            });
        }

        public ComicDto GetComicById(int id)
        {
            var comic = _comicRepository.GetComicById(id);
            if (comic == null) return null;

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
                UpdatedAt = comic.UpdatedAt
            };
        }

        public void AddComic(Comic comic)
        {
            _comicRepository.AddComic(comic);
        }

        public void UpdateComic(Comic comic)
        {
            _comicRepository.UpdateComic(comic);
        }

        public void DeleteComic(int id)
        {
            _comicRepository.DeleteComic(id);
        }

        public async Task<IEnumerable<ComicDto>> SearchComicsAsync(string? title, int? categoryId, int? classId)
        {
            var comics = await _comicRepository.SearchComicsAsync(title, categoryId, classId);
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
                UpdatedAt = c.UpdatedAt
            });
        }
    }
}