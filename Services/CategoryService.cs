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
                ClassId = c.class_id
            }).ToList();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return new CategoryDto
            {
                Id = category.category_id,
                Name = category.name,
                Description = category.description,
                ClassId = category.class_id
            };
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                category_id = categoryDto.Id,
                name = categoryDto.Name,
                description = categoryDto.Description,
                class_id = categoryDto.ClassId
            };
            var newCategory = await _categoryRepository.AddCategoryAsync(category);
            return new CategoryDto
            {
                Id = newCategory.category_id,
                Name = newCategory.name,
                Description = newCategory.description,
                ClassId = newCategory.class_id
            };
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                category_id = categoryDto.Id,
                name = categoryDto.Name,
                description = categoryDto.Description,
                class_id = categoryDto.ClassId
            };
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);
            return new CategoryDto
            {
                Id = updatedCategory.category_id,
                Name = updatedCategory.name,
                Description = updatedCategory.description,
                ClassId = updatedCategory.class_id
            };
        }

        public async Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string keyword)
        {
            var categories = await _categoryRepository.SearchCategoriesAsync(keyword);
            return categories.Select(c => new CategoryDto
            {
                Id = c.category_id,
                Name = c.name,
                Description = c.description,
                ClassId = c.class_id
            }).ToList();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
