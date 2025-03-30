using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IComicService
    {
        IEnumerable<ComicDto> GetAllComics();
        ComicDto GetComicById(int id);
        void AddComic(Comic comic);
        void UpdateComic(Comic comic);
        void DeleteComic(int id);
        Task<IEnumerable<ComicDto>> SearchComicsAsync(string? title, int? categoryId, int? classId);
    }
}