using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    namespace TaiLieuWebsiteBackend.Services
    {
        public class CommentService : ICommentService
        {
            private readonly ICommentRepository _commentRepository;

            public CommentService(ICommentRepository commentRepository)
            {
                _commentRepository = commentRepository;
            }

            public async Task<Comment> CreateCommentAsync(string content, int userId,
    int? documentId = null, int? gameId = null, int? videoId = null, int? comicId = null)
            {
                int providedIds = new[] { documentId, gameId, videoId, comicId }
                    .Count(id => id.HasValue && id != 0); // Check for non-zero values

                if (providedIds != 1)
                    throw new ArgumentException("Exactly one non-zero ID (document, game, video, or comic) must be provided");

                var comment = new Comment
                {
                    content = content,
                    user_id = userId,
                    document_id = documentId != 0 ? documentId : null,
                    game_id = gameId != 0 ? gameId : null,
                    video_id = videoId != 0 ? videoId : null,
                    comic_id = comicId != 0 ? comicId : null,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                return await _commentRepository.AddAsync(comment);
            }

            public async Task<Comment> UpdateCommentAsync(int commentId, string content, int userId)
            {
                var comment = await _commentRepository.GetByIdAsync(commentId);
                if (comment == null)
                    throw new Exception("Comment not found");

                if (comment.user_id != userId)
                    throw new Exception("Only the comment creator can update it");

                comment.content = content;
                comment.UpdatedAt = DateTime.Now;

                return await _commentRepository.UpdateAsync(comment);
            }

            public async Task DeleteCommentAsync(int commentId, int userId)
            {
                var comment = await _commentRepository.GetByIdAsync(commentId);
                if (comment == null)
                    throw new Exception("Comment not found");

                if (comment.user_id != userId)
                    throw new Exception("Only the comment creator can delete it");

                await _commentRepository.DeleteAsync(commentId);
            }

            public async Task<List<Comment>> GetCommentsByIdAsync(int? documentId = null,
                int? gameId = null, int? videoId = null, int? comicId = null)
            {
                if (documentId.HasValue)
                    return await _commentRepository.GetByDocumentIdAsync(documentId.Value);
                if (gameId.HasValue)
                    return await _commentRepository.GetByGameIdAsync(gameId.Value);
                if (videoId.HasValue)
                    return await _commentRepository.GetByVideoIdAsync(videoId.Value);
                if (comicId.HasValue)
                    return await _commentRepository.GetByComicIdAsync(comicId.Value);

                throw new Exception("Please provide at least one ID");
            }

            public async Task<int> CountCommentsAsync(int? documentId = null,
                int? gameId = null, int? videoId = null, int? comicId = null)
            {
                if (documentId.HasValue)
                    return await _commentRepository.CountByDocumentIdAsync(documentId.Value);
                if (gameId.HasValue)
                    return await _commentRepository.CountByGameIdAsync(gameId.Value);
                if (videoId.HasValue)
                    return await _commentRepository.CountByVideoIdAsync(videoId.Value);
                if (comicId.HasValue)
                    return await _commentRepository.CountByComicIdAsync(comicId.Value);

                throw new Exception("Please provide at least one ID");
            }
        }
    }
}