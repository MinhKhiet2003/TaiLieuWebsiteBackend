using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.DTOs;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string keyword);

    }
}
