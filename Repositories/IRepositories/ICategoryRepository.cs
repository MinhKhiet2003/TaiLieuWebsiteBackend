using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<Category>> SearchCategoriesAsync(string keyword);
        Task<IEnumerable<Category>> GetCategoriesByClassIdAsync(int classId);
        Task<IEnumerable<Category>> GetCategoriesByClassIdAsyncSearch(int classId);
        Task<IEnumerable<Class>> GetUsedClassesAsync();
        Task<Dictionary<int, int>> CountCategoriesByClassAsync();
        Task<IEnumerable<Category>> GetUsedCategoriesByResourceTypeAsync(string resourceType, int? classId = null);
        Task<IEnumerable<CategorySimpleDto>> GetUsedCategoriesSimpleAsync(int? classId = null);
        Task RestoreCategoryAsync(int id);
    }
}
