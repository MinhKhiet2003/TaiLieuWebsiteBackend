using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Repositories.IRepositories;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DocumentController(IDocumentService documentService, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _documentService = documentService;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
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
        [HttpPost]
        public ActionResult AddDocument([FromBody] CreateUpdateDocumentDto documentDto)
        {
            var document = new Document
            {
                title = documentDto.Title,
                description = documentDto.Description,
                file_path = documentDto.file_path,
                category_id = documentDto.CategoryId,
                uploaded_by = documentDto.UploadedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _documentService.AddDocument(document);
                return CreatedAtAction(nameof(GetDocumentById), new { id = document.document_id }, document);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDocument(int id, [FromBody] CreateUpdateDocumentDto documentDto)
        {
            var document = new Document
            {
                document_id = id,
                title = documentDto.Title,
                description = documentDto.Description,
                file_path = documentDto.file_path,
                category_id = documentDto.CategoryId,
                uploaded_by = documentDto.UploadedBy,
                UpdatedAt = DateTime.Now
            };

            try
            {
                _documentService.UpdateDocument(document);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDocument(int id)
        {
            _documentService.DeleteDocument(id);
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchDocuments(
            [FromQuery] string? name,
            [FromQuery] int? categoryId,
            [FromQuery] int? classId)
        {
            var documents = await _documentService.SearchDocumentsAsync(name, categoryId, classId);
            return Ok(documents);
        }

    }
}
