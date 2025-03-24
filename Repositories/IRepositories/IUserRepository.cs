using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Response;

public interface IUserRepository
{
    Task<ApiResponse<int>> GetUserCountAsync();
    Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync();
    Task<ApiResponse<User>> GetUserByIdAsync(int id);
    Task<ApiResponse<object>> AddUserAsync(User user);
    Task<ApiResponse<object>> UpdateUserAsync(User user);
    Task<ApiResponse<object>> DeleteUserAsync(int id);
    Task<ApiResponse<bool>> UserExistsAsync(int id);
    Task<bool> EmailExistsAsync(string email);
    Task<ApiResponse<User>> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    Task<ApiResponse<object>> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
}
