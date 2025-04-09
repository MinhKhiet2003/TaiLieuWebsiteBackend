using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateUpdateCategoryDto categoryDto)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var existingCategories = categories.FirstOrDefault(c => string.Equals(c.Name, categoryDto.Name, StringComparison.OrdinalIgnoreCase) && c.ClassId == categoryDto.ClassId);

            if (existingCategories != null)
            {
                return BadRequest(new { message = "Đã có danh mục có tiêu đề tương tự trong hệ thống!" });
            }

                var category = new Category
            {
                name = categoryDto.Name,
                description = categoryDto.Description,
                class_id = categoryDto.ClassId,
                uploaded_by = categoryDto.UploadedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            try
            {
                 await _categoryService.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.category_id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateUpdateCategoryDto categoryDto)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var existingCategory = categories
                .FirstOrDefault(c => string.Equals(c.Name, categoryDto.Name, StringComparison.OrdinalIgnoreCase) && c.Id != id && c.ClassId == categoryDto.ClassId);

            if (existingCategory != null)
            {
                return BadRequest(new { message = "Đã có Danh mục có tiêu đề tương tự trong danh mục này!" });
            }
            
            var category = new Category
            {
                category_id = id,
                name = categoryDto.Name,
                description = categoryDto.Description,
                class_id = categoryDto.ClassId,
                uploaded_by = categoryDto.UploadedBy,
                UpdatedAt = DateTime.Now
            };

            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCategories([FromQuery] string keyword)
        {
            var categories = await _categoryService.SearchCategoriesAsync(keyword);
            return Ok(categories);
        }

        [HttpGet("by-class/{classId}")]
        public async Task<IActionResult> GetCategoriesByClassId(int classId)
        {
            var categories = await _categoryService.GetCategoriesByClassIdAsync(classId);
            if (categories == null || !categories.Any())
            {
                return NotFound("Không tìm thấy danh mục nào cho classId này.");
            }
            return Ok(categories);
        }
        [HttpGet("used-classes")]
        public async Task<IActionResult> GetUsedClasses()
        {
            var classes = await _categoryService.GetUsedClassesAsync();
            return Ok(classes);
        }
        [HttpGet("count-by-class")]
        public async Task<IActionResult> CountCategoriesByClass()
        {
            var counts = await _categoryService.CountCategoriesByClassAsync();
            return Ok(counts);
        }
        [HttpGet("used-by-type")]
        public async Task<IActionResult> GetUsedCategoriesByResourceType([FromQuery] string resourceType, [FromQuery] int? classId)
        {
            var categories = await _categoryService.GetUsedCategoriesByResourceTypeAsync(resourceType, classId);
            return Ok(categories);
        }

        [HttpGet("used-simple")]
        public async Task<IActionResult> GetUsedCategoriesSimple([FromQuery] int? classId)
        {
            var categories = await _categoryService.GetUsedCategoriesSimpleAsync(classId);
            return Ok(categories);
        }
    }
}
