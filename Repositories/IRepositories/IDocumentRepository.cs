using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAllDocuments();
        Document GetDocumentById(int id);
        void AddDocument(Document document);
        void UpdateDocument(Document document);
        void DeleteDocument(int id);
        IEnumerable<Document> GetDocumentsByUploaderUsername(string username);
        IEnumerable<Document> SearchDocuments(string title = null, int? categoryId = null, string uploadedByUsername = null);
        Task<IEnumerable<Document>> SearchDocumentsAsync(string name, int? categoryId, int? classId);

    }

}
