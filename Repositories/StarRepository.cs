using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class StarRepository : IStarRepository
    {
        private readonly AppDbContext _context;

        public StarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Star> AddAsync(Star star)
        {
            _context.Stars.Add(star);
            await _context.SaveChangesAsync();
            return star;
        }

        public async Task<Star> UpdateAsync(Star star)
        {
            _context.Stars.Update(star);
            await _context.SaveChangesAsync();
            return star;
        }

        public async Task DeleteAsync(int starId)
        {
            var star = await _context.Stars.FindAsync(starId);
            if (star != null)
            {
                _context.Stars.Remove(star);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Star> GetByIdAsync(int starId)
        {
            return await _context.Stars
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.star_id == starId);
        }

        public async Task<Star> GetByUserAndContentIdAsync(int userId, int? documentId = null,
    int? exerciseId = null, int? gameId = null, int? videoId = null, int? comicId = null)
        {
            if (documentId.HasValue && documentId != 0)
            {
                return await _context.Stars
                    .FirstOrDefaultAsync(s => s.user_id == userId &&
                                              s.document_id == documentId &&
                                              s.ContentType == "Document");
            }
            if (exerciseId.HasValue && exerciseId != 0)
            {
                return await _context.Stars
                    .FirstOrDefaultAsync(s => s.user_id == userId &&
                                              s.exercise_id == exerciseId &&
                                              s.ContentType == "Exercise");
            }
            if (gameId.HasValue && gameId != 0)
            {
                return await _context.Stars
                    .FirstOrDefaultAsync(s => s.user_id == userId &&
                                              s.game_id == gameId &&
                                              s.ContentType == "Game");
            }
            if (videoId.HasValue && videoId != 0)
            {
                return await _context.Stars
                    .FirstOrDefaultAsync(s => s.user_id == userId &&
                                              s.video_id == videoId &&
                                              s.ContentType == "Video");
            }
            if (comicId.HasValue && comicId != 0)
            {
                return await _context.Stars
                    .FirstOrDefaultAsync(s => s.user_id == userId &&
                                              s.comic_id == comicId &&
                                              s.ContentType == "Comic");
            }

            return null;
        }

        public async Task<List<Star>> GetByDocumentIdAsync(int documentId)
        {
            return await _context.Stars
                .Where(s => s.document_id == documentId && s.ContentType == "Document")
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<List<Star>> GetByExerciseIdAsync(int exerciseId)
        {
            return await _context.Stars
                .Where(s => s.exercise_id == exerciseId && s.ContentType == "Exercise")
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<List<Star>> GetByGameIdAsync(int gameId)
        {
            return await _context.Stars
                .Where(s => s.game_id == gameId && s.ContentType == "Game")
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<List<Star>> GetByVideoIdAsync(int videoId)
        {
            return await _context.Stars
                .Where(s => s.video_id == videoId && s.ContentType == "Video")
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<List<Star>> GetByComicIdAsync(int comicId)
        {
            return await _context.Stars
                .Where(s => s.comic_id == comicId && s.ContentType == "Comic")
                .Include(s => s.User)
                .ToListAsync();
        }

        public async Task<double> GetAverageByDocumentIdAsync(int documentId)
        {
            var ratings = await _context.Stars
                .Where(s => s.document_id == documentId && s.ContentType == "Document")
                .ToListAsync();
            return ratings.Any() ? ratings.Average(s => s.total_star) : 0;
        }

        public async Task<double> GetAverageByExerciseIdAsync(int exerciseId)
        {
            var ratings = await _context.Stars
                .Where(s => s.exercise_id == exerciseId && s.ContentType == "Exercise")
                .ToListAsync();
            return ratings.Any() ? ratings.Average(s => s.total_star) : 0;
        }

        public async Task<double> GetAverageByGameIdAsync(int gameId)
        {
            var ratings = await _context.Stars
                .Where(s => s.game_id == gameId && s.ContentType == "Game")
                .ToListAsync();
            return ratings.Any() ? ratings.Average(s => s.total_star) : 0;
        }

        public async Task<double> GetAverageByVideoIdAsync(int videoId)
        {
            var ratings = await _context.Stars
                .Where(s => s.video_id == videoId && s.ContentType == "Video")
                .ToListAsync();
            return ratings.Any() ? ratings.Average(s => s.total_star) : 0;
        }

        public async Task<double> GetAverageByComicIdAsync(int comicId)
        {
            var ratings = await _context.Stars
                .Where(s => s.comic_id == comicId && s.ContentType == "Comic")
                .ToListAsync();
            return ratings.Any() ? ratings.Average(s => s.total_star) : 0;
        }

        public Dictionary<int, double> GetAverageRatingsByDocumentIds(List<int> documentIds)
        {
            var ratings = _context.Stars
                .Where(s => s.ContentType == "Document" && documentIds.Contains(s.document_id.Value))
                .GroupBy(s => s.document_id)
                .Select(g => new
                {
                    DocumentId = g.Key.Value,
                    AverageRating = g.Average(s => s.total_star)
                })
                .ToDictionary(g => g.DocumentId, g => g.AverageRating);

            return documentIds.ToDictionary(
                id => id,
                id => ratings.TryGetValue(id, out double rating) ? rating : 0
            );
        }

        public async Task<Dictionary<int, double>> GetAverageRatingsByContentTypeAsync(string contentType)
        {
            switch (contentType)
            {
                case "Document":
                    return await _context.Stars
                        .Where(s => s.ContentType == "Document" && s.document_id.HasValue)
                        .GroupBy(s => s.document_id)
                        .Select(g => new
                        {
                            Id = g.Key!.Value,
                            AverageRating = g.Average(s => s.total_star)
                        })
                        .ToDictionaryAsync(g => g.Id, g => g.AverageRating);

                case "Exercise":
                    return await _context.Stars
                        .Where(s => s.ContentType == "Exercise" && s.exercise_id.HasValue)
                        .GroupBy(s => s.exercise_id)
                        .Select(g => new
                        {
                            Id = g.Key!.Value,
                            AverageRating = g.Average(s => s.total_star)
                        })
                        .ToDictionaryAsync(g => g.Id, g => g.AverageRating);

                case "Game":
                    return await _context.Stars
                        .Where(s => s.ContentType == "Game" && s.game_id.HasValue)
                        .GroupBy(s => s.game_id)
                        .Select(g => new
                        {
                            Id = g.Key!.Value,
                            AverageRating = g.Average(s => s.total_star)
                        })
                        .ToDictionaryAsync(g => g.Id, g => g.AverageRating);

                case "Video":
                    return await _context.Stars
                        .Where(s => s.ContentType == "Video" && s.video_id.HasValue)
                        .GroupBy(s => s.video_id)
                        .Select(g => new
                        {
                            Id = g.Key!.Value,
                            AverageRating = g.Average(s => s.total_star)
                        })
                        .ToDictionaryAsync(g => g.Id, g => g.AverageRating);

                case "Comic":
                    return await _context.Stars
                        .Where(s => s.ContentType == "Comic" && s.comic_id.HasValue)
                        .GroupBy(s => s.comic_id)
                        .Select(g => new
                        {
                            Id = g.Key!.Value,
                            AverageRating = g.Average(s => s.total_star)
                        })
                        .ToDictionaryAsync(g => g.Id, g => g.AverageRating);

                default:
                    throw new ArgumentException("Invalid content type.");
            }
        }

        public Dictionary<int, double> GetAverageRatingsByGameIds(List<int> gameIds)
        {
            var ratings = _context.Stars
                .Where(s => s.ContentType == "Game" && gameIds.Contains(s.game_id.Value))
                .GroupBy(s => s.game_id)
                .Select(g => new
                {
                    GameId = g.Key.Value,
                    AverageRating = g.Average(s => s.total_star)
                })
                .ToDictionary(g => g.GameId, g => g.AverageRating);

            return gameIds.ToDictionary(
                id => id,
                id => ratings.TryGetValue(id, out double rating) ? rating : 0
            );
        }

        public Dictionary<int, double> GetAverageRatingsByVideoIds(List<int> videoIds)
        {
            var ratings = _context.Stars
                .Where(s => s.ContentType == "Video" && videoIds.Contains(s.video_id.Value))
                .GroupBy(s => s.video_id)
                .Select(g => new
                {
                    VideoId = g.Key.Value,
                    AverageRating = g.Average(s => s.total_star)
                })
                .ToDictionary(g => g.VideoId, g => g.AverageRating);

            return videoIds.ToDictionary(
                id => id,
                id => ratings.TryGetValue(id, out double rating) ? rating : 0
            );
        }

        public Dictionary<int, double> GetAverageRatingsByComicIds(List<int> comicIds)
        {
            var ratings = _context.Stars
                .Where(s => s.ContentType == "Comic" && comicIds.Contains(s.comic_id.Value))
                .GroupBy(s => s.comic_id)
                .Select(g => new
                {
                    ComicId = g.Key.Value,
                    AverageRating = g.Average(s => s.total_star)
                })
                .ToDictionary(g => g.ComicId, g => g.AverageRating);

            return comicIds.ToDictionary(
                id => id,
                id => ratings.TryGetValue(id, out double rating) ? rating : 0
            );
        }
    }
}