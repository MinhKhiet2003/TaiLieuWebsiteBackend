using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IComicRepository
    {
        IEnumerable<Comic> GetAllComics();
        Comic? GetComicById(int id);
        void AddComic(Comic comic);
        void UpdateComic(Comic comic);
        void DeleteComic(int id);
        Task<IEnumerable<Comic>> SearchComicsAsync(string? title, int? categoryId, int? classId);
    }
}