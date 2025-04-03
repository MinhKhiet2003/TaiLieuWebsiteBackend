using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string keyword);
        Task<CategoryDto> GetCategoryByNameAsync(string name);
        Task<IEnumerable<CategoryDto>> GetCategoriesByClassIdAsync(int classId);
        Task<IEnumerable<ClassDto>> GetUsedClassesAsync();
        Task<Dictionary<int, int>> CountCategoriesByClassAsync();
        Task<IEnumerable<CategorySimpleDto>> GetUsedCategoriesByResourceTypeAsync(string resourceType, int? classId = null);
        Task<IEnumerable<CategorySimpleDto>> GetUsedCategoriesSimpleAsync(int? classId = null);
    }
}
