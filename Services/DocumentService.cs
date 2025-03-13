using System.Collections.Generic;
using System.Linq;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public IEnumerable<DocumentDto> GetAllDocuments()
        {
            var documents = _documentRepository.GetAllDocuments();
            return documents.Select(d => new DocumentDto
            {
                Id = d.document_id,
                Title = d.title,
                Description = d.description,
                file_path = d.file_path,
                CategoryId = d.category_id,
                UploadedBy = d.uploaded_by,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            }).ToList();
        }

        public DocumentDto GetDocumentById(int id)
        {
            var document = _documentRepository.GetDocumentById(id);
            if (document == null)
            {
                return null;
            }
            return new DocumentDto
            {
                Id = document.document_id,
                Title = document.title,
                Description = document.description,
                file_path = document.file_path,
                CategoryId = document.category_id,
                UploadedBy = document.uploaded_by,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };
        }

        public void AddDocument(Document document)
        {
            _documentRepository.AddDocument(document);
        }

        public void UpdateDocument(Document document)
        {
            _documentRepository.UpdateDocument(document);
        }

        public void DeleteDocument(int id)
        {
            _documentRepository.DeleteDocument(id);
        }

        public IEnumerable<DocumentDto> SearchDocuments(string title = null, int? categoryId = null, string uploadedByUsername = null)
        {
            var documents = _documentRepository.SearchDocuments(title, categoryId, uploadedByUsername);
            return documents.Select(d => new DocumentDto
            {
                Id = d.document_id,
                Title = d.title,
                Description = d.description,
                file_path = d.file_path,
                CategoryId = d.category_id,
                UploadedBy = d.uploaded_by,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            }).ToList();
        }
    }
}
