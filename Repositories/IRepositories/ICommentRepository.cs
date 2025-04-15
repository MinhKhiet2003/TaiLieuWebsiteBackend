using TaiLieuWebsiteBackend.Models;
using System.Collections.Generic;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task DeleteAsync(int commentId);
        Task<Comment> GetByIdAsync(int commentId);
        Task<List<Comment>> GetByDocumentIdAsync(int documentId);
        Task<List<Comment>> GetByGameIdAsync(int gameId);
        Task<List<Comment>> GetByVideoIdAsync(int videoId);
        Task<List<Comment>> GetByComicIdAsync(int comicId);
        Task<List<Comment>> GetByQuestionSetAndCategoryAsync(int questionSet, int categoryId);
        Task<int> CountByDocumentIdAsync(int documentId);
        Task<int> CountByGameIdAsync(int gameId);
        Task<int> CountByVideoIdAsync(int videoId);
        Task<int> CountByComicIdAsync(int comicId);
        Task<int> CountByQuestionSetAndCategoryAsync(int questionSet, int categoryId);
        Dictionary<int, int> GetCommentCountsByDocumentIds(IEnumerable<int> documentIds);
        Dictionary<int, int> GetCommentCountsByGameIds(List<int> gameIds);
        Dictionary<int, int> GetCommentCountsByVideoIds(List<int> videoIds);
        Dictionary<int, int> GetCommentCountsByComicIds(List<int> comicIds);
    }
}