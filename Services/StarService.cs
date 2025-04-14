using System;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class StarService : IStarService
    {
        private readonly IStarRepository _starRepository;

        public StarService(IStarRepository starRepository)
        {
            _starRepository = starRepository;
        }

        public async Task<Star> CreateOrUpdateRatingAsync(
             int rating,
             int userId,
             int? documentId = null,
             int? exerciseId = null,
             int? gameId = null,
             int? videoId = null,
             int? comicId = null)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Điểm đánh giá phải từ 1 đến 5");

            int providedIds = new[] { documentId, exerciseId, gameId, videoId, comicId }
                .Count(id => id.HasValue && id != 0);

            if (providedIds != 1)
                throw new ArgumentException("Exactly one non-zero ID (document, exercise, game, video, or comic) must be provided");

            string contentType = documentId.HasValue && documentId != 0 ? "Document" :
                                exerciseId.HasValue && exerciseId != 0 ? "Exercise" :
                                gameId.HasValue && gameId != 0 ? "Game" :
                                videoId.HasValue && videoId != 0 ? "Video" :
                                comicId.HasValue && comicId != 0 ? "Comic" :
                                throw new ArgumentException("Invalid content type");

            var existingRating = await _starRepository.GetByUserAndContentIdAsync(
                userId, documentId, exerciseId, gameId, videoId, comicId);

            if (existingRating != null)
            {
                // Update existing rating
                existingRating.total_star = rating;
                return await _starRepository.UpdateAsync(existingRating);
            }
            else
            {
                // Create new rating
                var star = new Star
                {
                    total_star = rating,
                    user_id = userId,
                    ContentType = contentType,
                    document_id = documentId != 0 ? documentId : null,
                    exercise_id = exerciseId != 0 ? exerciseId : null,
                    game_id = gameId != 0 ? gameId : null,
                    video_id = videoId != 0 ? videoId : null,
                    comic_id = comicId != 0 ? comicId : null
                };
                return await _starRepository.AddAsync(star);
            }
        }

        public async Task<List<Star>> GetRatingsByIdAsync(int? documentId = null,
            int? exerciseId = null, int? gameId = null, int? videoId = null, int? comicId = null)
        {
            if (documentId.HasValue)
                return await _starRepository.GetByDocumentIdAsync(documentId.Value);
            if (exerciseId.HasValue)
                return await _starRepository.GetByExerciseIdAsync(exerciseId.Value);
            if (gameId.HasValue)
                return await _starRepository.GetByGameIdAsync(gameId.Value);
            if (videoId.HasValue)
                return await _starRepository.GetByVideoIdAsync(videoId.Value);
            if (comicId.HasValue)
                return await _starRepository.GetByComicIdAsync(comicId.Value);

            throw new Exception("Please provide at least one ID");
        }

        public async Task<double> GetAverageRatingAsync(int? documentId = null,
            int? exerciseId = null, int? gameId = null, int? videoId = null, int? comicId = null)
        {
            if (documentId.HasValue)
                return await _starRepository.GetAverageByDocumentIdAsync(documentId.Value);
            if (exerciseId.HasValue)
                return await _starRepository.GetAverageByExerciseIdAsync(exerciseId.Value);
            if (gameId.HasValue)
                return await _starRepository.GetAverageByGameIdAsync(gameId.Value);
            if (videoId.HasValue)
                return await _starRepository.GetAverageByVideoIdAsync(videoId.Value);
            if (comicId.HasValue)
                return await _starRepository.GetAverageByComicIdAsync(comicId.Value);

            throw new Exception("Please provide at least one ID");
        }

        public async Task<Star> GetRatingByUserAndContentAsync(int userId, int? documentId = null,
    int? exerciseId = null, int? gameId = null, int? videoId = null, int? comicId = null)
        {
            int providedIds = new[] { documentId, exerciseId, gameId, videoId, comicId }
                .Count(id => id.HasValue && id != 0);

            if (providedIds != 1)
                throw new ArgumentException("Exactly one non-zero ID (document, exercise, game, video, or comic) must be provided");

            return await _starRepository.GetByUserAndContentIdAsync(userId, documentId, exerciseId, gameId, videoId, comicId);
        }

        public async Task<Dictionary<int, double>> GetAverageRatingsByContentTypeAsync(string contentType)
        {
            contentType = contentType.ToLower();
            switch (contentType)
            {
                case "document":
                case "exercise":
                case "game":
                case "video":
                case "comic":
                    return await _starRepository.GetAverageRatingsByContentTypeAsync(
                        char.ToUpper(contentType[0]) + contentType.Substring(1));
                default:
                    throw new ArgumentException("Invalid content type. Must be one of: document, exercise, game, video, comic.");
            }
        }

    }

}