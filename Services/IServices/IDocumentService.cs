using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IDocumentService
    {
        IEnumerable<DocumentDto> GetAllDocuments();
        DocumentDto GetDocumentById(int id);
        void AddDocument(Document document);
        void UpdateDocument(Document document);
        void DeleteDocument(int id);
        Task<IEnumerable<DocumentDto>> SearchDocumentsAsync(string name, int? categoryId, int? classId);

    }

}
