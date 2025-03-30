using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Repositories.IRepositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
    }
}
