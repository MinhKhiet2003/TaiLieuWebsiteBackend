using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DocumentDto>> GetAllDocuments()
        {
            var documents = _documentService.GetAllDocuments();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public ActionResult<DocumentDto> GetDocumentById(int id)
        {
            var document = _documentService.GetDocumentById(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<DocumentDto>> SearchDocuments([FromQuery] string title, [FromQuery] int? categoryId, [FromQuery] string uploadedByUsername)
        {
            var documents = _documentService.SearchDocuments(title, categoryId, uploadedByUsername);
            return Ok(documents);
        }

        [HttpPost]
        public ActionResult AddDocument([FromBody] DocumentDto documentDto)
        {
            var document = new Document
            {
                title = documentDto.Title,
                description = documentDto.Description,
                file_path = documentDto.file_path,
                category_id = documentDto.CategoryId,
                uploaded_by = documentDto.UploadedBy,
                CreatedAt = documentDto.CreatedAt,
                UpdatedAt = documentDto.UpdatedAt
            };

            _documentService.AddDocument(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.document_id }, document);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDocument(int id, [FromBody] DocumentDto documentDto)
        {
            if (id != documentDto.Id)
            {
                return BadRequest();
            }

            var document = new Document
            {
                document_id = documentDto.Id,
                title = documentDto.Title,
                description = documentDto.Description,
                file_path = documentDto.file_path,
                category_id = documentDto.CategoryId,
                uploaded_by = documentDto.UploadedBy,
                CreatedAt = documentDto.CreatedAt,
                UpdatedAt = documentDto.UpdatedAt
            };

            _documentService.UpdateDocument(document);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDocument(int id)
        {
            _documentService.DeleteDocument(id);
            return NoContent();
        }
    }
}
