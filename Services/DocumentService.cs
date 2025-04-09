using System.Collections.Generic;
using System.Linq;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;
using System;

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
                UploadedByUsername = d.User?.username ?? "Không xác định",
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            }).ToList();
        }

        public DocumentDto? GetDocumentById(int id)
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
                UploadedByUsername = document.User?.username ?? "Không xác định",
                UploadedBy = document.uploaded_by,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt
            };
        }

        public void AddDocument(Document document)
        {
            var existingDocument = _documentRepository.SearchDocuments(document.title, document.category_id, null).FirstOrDefault();
            if (existingDocument != null)
            {
                throw new Exception("Đã có tài liệu có tiêu đề tương tự !");
            }

            document.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            document.UpdatedAt = document.CreatedAt; 
            _documentRepository.AddDocument(document);
        }

        public void UpdateDocument(Document document)
        {
            var existingDocument = _documentRepository.GetDocumentById(document.document_id);
            if (existingDocument != null)
            {
                var duplicateDocument = _documentRepository.SearchDocuments(document.title, document.category_id, null)
                    .FirstOrDefault(d => d.document_id != document.document_id);
                if (duplicateDocument != null)
                {
                    throw new Exception("Đã có tài liệu có tiêu đề tương tự !");
                }

                document.CreatedAt = existingDocument.CreatedAt;
                document.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")); // Cập nhật giá trị UpdatedAt với múi giờ +7 Hà Nội
                _documentRepository.UpdateDocument(document);
            }
        }

        public void DeleteDocument(int id)
        {
            _documentRepository.DeleteDocument(id);
        }

        public async Task<IEnumerable<DocumentDto>> SearchDocumentsAsync(string? name, int? categoryId, int? classId)
        {
            var documents = await _documentRepository.SearchDocumentsAsync(name, categoryId, classId);
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
        public IEnumerable<DocumentDto> GetDocumentsByCategoryId(int categoryId)
        {
            var documents = _documentRepository.GetDocumentsByCategoryId(categoryId);
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
        public async Task<IEnumerable<int>> GetUsedCategoryIdsAsync()
        {
            var documents = _documentRepository.GetAllDocuments();
            return documents.Select(d => d.category_id).Distinct();
        }
    }
}
