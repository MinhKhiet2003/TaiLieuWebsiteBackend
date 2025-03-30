using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.category_id,
                Name = c.name,
                Description = c.description,
                ClassId = c.class_id,
                UploadedBy = c.uploaded_by,
                UploadedByUsername = c.User?.username,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt

            }).ToList();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id); 
            if (category == null)
            {
                return null;
            }
            return new CategoryDto
            {
                Id = category.category_id,
                Name = category.name,
                Description = category.description,
                ClassId = category.class_id,
                UploadedBy = category.uploaded_by,
                UploadedByUsername = category.User?.username,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }

        public async Task AddCategoryAsync(Category category)
        {
             await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
               await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string keyword)
        {
            var categories = await _categoryRepository.SearchCategoriesAsync(keyword);
            return categories
                .Where(c => c.name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Select(c => new CategoryDto
                {
                    Id = c.category_id,
                    Name = c.name,
                    Description = c.description,
                    ClassId = c.class_id
                })
                .ToList();
        }

        public async Task DeleteCategoryAsync(int id)
        {
             await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<CategoryDto> GetCategoryByNameAsync(string name)
        {
            var category = await _categoryRepository.GetCategoryByName(name);
            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = category.category_id,
                Name = category.name,
                Description = category.description,
                ClassId = category.class_id
            };
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesByClassIdAsync(int classId)
        {
            var categories = await _categoryRepository.GetCategoriesByClassIdAsync(classId);
            return categories.Select(c => new CategoryDto
            {
                Id = c.category_id,
                Name = c.name,
                Description = c.description,
                ClassId = c.class_id,
                UploadedBy = c.uploaded_by,
                UploadedByUsername = c.User?.username, 
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }
        public async Task<IEnumerable<ClassDto>> GetUsedClassesAsync()
        {
            var classes = await _categoryRepository.GetUsedClassesAsync();
            return classes.Select(c => new ClassDto
            {
                Id = c.class_id,
                Name = c.name
            }).ToList();
        }
        public async Task<Dictionary<int, int>> CountCategoriesByClassAsync()
        {
            return await _categoryRepository.CountCategoriesByClassAsync();
        }
    }
}
