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
        private readonly ICommentRepository _commentRepository;
        private readonly IStarRepository _starRepository;
        public DocumentService(
             IDocumentRepository documentRepository,
             ICommentRepository commentRepository,
             IStarRepository starRepository)
        {
            _documentRepository = documentRepository;
            _commentRepository = commentRepository;
            _starRepository = starRepository;
        }

        public IEnumerable<DocumentDto> GetAllDocuments()
        {
            var documents = _documentRepository.GetAllDocuments();
            var documentIds = documents.Select(d => d.document_id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByDocumentIds(documentIds);
            var averageRatings = _starRepository.GetAverageRatingsByDocumentIds(documentIds);
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
                UpdatedAt = d.UpdatedAt,
                CommentCount = commentCounts.TryGetValue(d.document_id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(d.document_id, out double rating) ? rating : 0
            }).ToList();
        }

        public DocumentDto? GetDocumentById(int id)
        {
            var document = _documentRepository.GetDocumentById(id);
            if (document == null)
            {
                return null;
            }

            var commentCount = _commentRepository.CountByDocumentIdAsync(id).Result;
            var averageRating = _starRepository.GetAverageByDocumentIdAsync(id).Result;

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
                UpdatedAt = document.UpdatedAt,
                CommentCount = commentCount,
                AverageRating = averageRating
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
            var documentIds = documents.Select(d => d.document_id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByDocumentIds(documentIds);
            var averageRatings = _starRepository.GetAverageRatingsByDocumentIds(documentIds);

            return documents.Select(d => new DocumentDto
            {
                Id = d.document_id,
                Title = d.title,
                Description = d.description,
                file_path = d.file_path,
                CategoryId = d.category_id,
                UploadedBy = d.uploaded_by,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt,
                UploadedByUsername = d.User?.username ?? "Không xác định",
                CommentCount = commentCounts.TryGetValue(d.document_id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(d.document_id, out double rating) ? rating : 0
            }).ToList();
        }

        public IEnumerable<DocumentDto> GetDocumentsByCategoryId(int categoryId)
        {
            var documents = _documentRepository.GetDocumentsByCategoryId(categoryId);
            var documentIds = documents.Select(d => d.document_id).ToList();
            var commentCounts = _commentRepository.GetCommentCountsByDocumentIds(documentIds);
            var averageRatings = _starRepository.GetAverageRatingsByDocumentIds(documentIds);

            return documents.Select(d => new DocumentDto
            {
                Id = d.document_id,
                Title = d.title,
                Description = d.description,
                file_path = d.file_path,
                CategoryId = d.category_id,
                UploadedBy = d.uploaded_by,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt,
                CommentCount = commentCounts.TryGetValue(d.document_id, out int count) ? count : 0,
                AverageRating = averageRatings.TryGetValue(d.document_id, out double rating) ? rating : 0
            }).ToList();
        }
        public async Task<IEnumerable<int>> GetUsedCategoryIdsAsync()
        {
            var documents = _documentRepository.GetAllDocuments();
            return documents.Select(d => d.category_id).Distinct();
        }
    }
}
