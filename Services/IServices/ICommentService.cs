using TaiLieuWebsiteBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(string content, int userId,
            int? documentId = null, int? gameId = null, int? videoId = null,
            int? comicId = null, int? questionSet = null, int? categoryId = null);

        Task<Comment> UpdateCommentAsync(int commentId, string content, int userId);
        Task DeleteCommentAsync(int commentId, int userId);
        Task<List<Comment>> GetCommentsByIdAsync(
            int? documentId = null, int? gameId = null,
            int? videoId = null, int? comicId = null,
            int? questionSet = null, int? categoryId = null);

        Task<int> CountCommentsAsync(
            int? documentId = null, int? gameId = null,
            int? videoId = null, int? comicId = null,
            int? questionSet = null, int? categoryId = null);

        Task<List<Comment>> GetCommentsByQuestionSetAsync(int questionSet, int categoryId);
        Task<int> CountCommentsByQuestionSetAsync(int questionSet, int categoryId);
    }
}