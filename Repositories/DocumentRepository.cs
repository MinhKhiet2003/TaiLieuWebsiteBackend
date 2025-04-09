using System.Collections.Generic;
using System.Linq;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Document> GetAllDocuments()
        {
            return _context.Documents.Include(d => d.Category).Include(d => d.User).ToList();
        }

        public Document? GetDocumentById(int id)
        {
            return _context.Documents.Include(d => d.Category).Include(d => d.User).FirstOrDefault(d => d.document_id == id);
        }

        public void AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
        }

        public void UpdateDocument(Document document)
        {
            var existingDocument = _context.Documents.Local.FirstOrDefault(d => d.document_id == document.document_id);
            if (existingDocument != null)
            {
                _context.Entry(existingDocument).State = EntityState.Detached;
            }
            _context.Documents.Update(document);
            _context.SaveChanges();
        }

        public void DeleteDocument(int id)
        {
            var document = _context.Documents.Find(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Document> GetDocumentsByUploaderUsername(string username)
        {
            return _context.Documents.Include(d => d.User).Where(d => d.User.username == username).ToList();
        }

        public IEnumerable<Document> SearchDocuments(string? title = null, int? categoryId = null, string? uploadedByUsername = null)
        {
            var query = _context.Documents.Include(d => d.Category).Include(d => d.User).AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(d => d.title.Contains(title));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(d => d.category_id == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(uploadedByUsername))
            {
                query = query.Where(d => d.User.username == uploadedByUsername);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<Document>> SearchDocumentsAsync(string? name, int? categoryId, int? classId)
        {
            var query = _context.Documents.AsQueryable();

            // Lọc theo tên (nếu có)
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(d => d.title.Contains(name));
            }

            // Lọc theo category (nếu có)
            if (categoryId.HasValue)
            {
                query = query.Where(d => d.category_id == categoryId);
            }

            // Lọc theo class (nếu có) -> Thông qua bảng Categories
            if (classId.HasValue)
            {
                query = query.Where(d => d.Category.class_id == classId);
            }

            return await query.ToListAsync();
        }
        public IEnumerable<Document> GetDocumentsByCategoryId(int categoryId)
        {
            return _context.Documents
                .Include(d => d.Category)
                .Include(d => d.User)
                .Where(d => d.category_id == categoryId)
                .ToList();
        }
    }
}
