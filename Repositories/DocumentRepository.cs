using System.Collections.Generic;
using System.Linq;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

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
            return _context.Documents;
        }

        public Document GetDocumentById(int id)
        {
            return _context.Documents.Find(id);
        }

        public void AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
        }

        public void UpdateDocument(Document document)
        {
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
            return _context.Documents.Where(d => d.User.username == username).ToList();
        }

        public IEnumerable<Document> SearchDocuments(string title = null, int? categoryId = null, string uploadedByUsername = null)
        {
            var query = _context.Documents.AsQueryable();

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
    }
}
