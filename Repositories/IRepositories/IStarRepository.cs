using System.Linq.Expressions;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IStarRepository
    {
        Task<Star> AddAsync(Star star);
        Task<Star> UpdateAsync(Star star);
        Task DeleteAsync(int starId);
        Task<Star> GetByIdAsync(int starId);
        Task<Star> GetByUserAndContentIdAsync(
            int userId,
            int? documentId = null,
            int? exerciseId = null,
            int? gameId = null,
            int? videoId = null,
            int? comicId = null);
        Task<List<Star>> GetByDocumentIdAsync(int documentId);
        Task<List<Star>> GetByExerciseIdAsync(int exerciseId);
        Task<List<Star>> GetByGameIdAsync(int gameId);
        Task<List<Star>> GetByVideoIdAsync(int videoId);
        Task<List<Star>> GetByComicIdAsync(int comicId);
        Task<double> GetAverageByDocumentIdAsync(int documentId);
        Task<double> GetAverageByExerciseIdAsync(int exerciseId);
        Task<double> GetAverageByGameIdAsync(int gameId);
        Task<double> GetAverageByVideoIdAsync(int videoId);
        Task<double> GetAverageByComicIdAsync(int comicId);
        Dictionary<int, double> GetAverageRatingsByDocumentIds(List<int> documentIds);
        Task<Dictionary<int, double>> GetAverageRatingsByContentTypeAsync(string contentType);
        Dictionary<int, double> GetAverageRatingsByGameIds(List<int> gameIds);
        Dictionary<int, double> GetAverageRatingsByVideoIds(List<int> videoIds);
        Dictionary<int, double> GetAverageRatingsByComicIds(List<int> comicIds);
    }
}
