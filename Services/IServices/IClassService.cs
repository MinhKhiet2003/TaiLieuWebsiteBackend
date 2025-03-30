using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
    }
}
