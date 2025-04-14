using Microsoft.EntityFrameworkCore;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.comment_id == commentId);
        }

        public async Task<List<Comment>> GetByDocumentIdAsync(int documentId)
        {
            return await _context.Comments
                .Where(c => c.document_id == documentId)
                .Include(c => c.User)
                .ToListAsync();
        }
        public Dictionary<int, int> GetCommentCountsByDocumentIds(IEnumerable<int> documentIds)
        {
            return _context.Comments
                .Where(c => c.document_id != null && documentIds.Contains(c.document_id.Value))
                .GroupBy(c => c.document_id)
                .ToDictionary(g => g.Key.Value, g => g.Count());
        }
        public async Task<List<Comment>> GetByGameIdAsync(int gameId)
        {
            return await _context.Comments
                .Where(c => c.game_id == gameId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public Dictionary<int, int> GetCommentCountsByGameIds(List<int> gameIds)
        {
            return _context.Comments
                .Where(c => gameIds.Contains(c.game_id.Value))
                .GroupBy(c => c.game_id.Value)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public async Task<int> CountByVideoIdAsync(int videoId)
        {
            return await _context.Comments
                .CountAsync(c => c.video_id == videoId);
        }

        public Dictionary<int, int> GetCommentCountsByVideoIds(List<int> videoIds)
        {
            return _context.Comments
                .Where(c => videoIds.Contains(c.video_id.Value))
                .GroupBy(c => c.video_id.Value)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public async Task<List<Comment>> GetByVideoIdAsync(int videoId)
        {
            return await _context.Comments
                .Where(c => c.video_id == videoId)
                .Include(c => c.User)
                .ToListAsync();
        }
        public Dictionary<int, int> GetCommentCountsByComicIds(List<int> comicIds)
        {
            return _context.Comments
                .Where(c => c.comic_id.HasValue && comicIds.Contains(c.comic_id.Value))
                .GroupBy(c => c.comic_id.Value)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public async Task<List<Comment>> GetByComicIdAsync(int comicId)
        {
            return await _context.Comments
                .Where(c => c.comic_id == comicId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<int> CountByDocumentIdAsync(int documentId)
        {
            return await _context.Comments.CountAsync(c => c.document_id == documentId);
        }

        public async Task<int> CountByGameIdAsync(int gameId)
        {
            return await _context.Comments.CountAsync(c => c.game_id == gameId);
        }
        public async Task<int> CountByComicIdAsync(int comicId)
        {
            return await _context.Comments.CountAsync(c => c.comic_id == comicId);
        }
    }
}