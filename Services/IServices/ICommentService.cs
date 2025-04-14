using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(string content, int userId, int? documentId = null,
            int? gameId = null, int? videoId = null, int? comicId = null);
        Task<Comment> UpdateCommentAsync(int commentId, string content, int userId);
        Task DeleteCommentAsync(int commentId, int userId);
        Task<List<Comment>> GetCommentsByIdAsync(int? documentId = null, int? gameId = null,
            int? videoId = null, int? comicId = null);
        Task<int> CountCommentsAsync(int? documentId = null, int? gameId = null,
            int? videoId = null, int? comicId = null);
    }
}
