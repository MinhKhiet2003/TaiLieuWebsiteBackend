using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task DeleteAsync(int commentId);
        Task<Comment> GetByIdAsync(int commentId);
        Dictionary<int, int> GetCommentCountsByDocumentIds(IEnumerable<int> documentIds);
        Task<List<Comment>> GetByDocumentIdAsync(int documentId);
        Dictionary<int, int> GetCommentCountsByGameIds(List<int> gameIds);
        Task<List<Comment>> GetByGameIdAsync(int gameId);
        Dictionary<int, int> GetCommentCountsByVideoIds(List<int> videoIds);
        Task<List<Comment>> GetByVideoIdAsync(int videoId);
        Dictionary<int, int> GetCommentCountsByComicIds(List<int> comicIds);
        Task<List<Comment>> GetByComicIdAsync(int comicId);
        Task<int> CountByDocumentIdAsync(int documentId);
        Task<int> CountByGameIdAsync(int gameId);
        Task<int> CountByVideoIdAsync(int videoId);
        Task<int> CountByComicIdAsync(int comicId);
    }
}
