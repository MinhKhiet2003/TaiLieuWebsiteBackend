using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IStarService
    {
        Task<Star> CreateOrUpdateRatingAsync(
             int rating,
             int userId,
             int? documentId = null,
             int? exerciseId = null,
             int? gameId = null,
             int? videoId = null,
             int? comicId = null);
        Task<List<Star>> GetRatingsByIdAsync(int? documentId = null, int? exerciseId = null,
            int? gameId = null, int? videoId = null, int? comicId = null);
        Task<double> GetAverageRatingAsync(int? documentId = null, int? exerciseId = null,
            int? gameId = null, int? videoId = null, int? comicId = null);
        Task<Star> GetRatingByUserAndContentAsync(int userId, int? documentId = null,
                int? exerciseId = null, int? gameId = null, int? videoId = null, int? comicId = null);
        Task<Dictionary<int, double>> GetAverageRatingsByContentTypeAsync(string contentType);
    }
}
