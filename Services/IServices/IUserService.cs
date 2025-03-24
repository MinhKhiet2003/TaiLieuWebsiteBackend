using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Response;

public interface IUserService
{
    Task<ApiResponse<int>> GetUserCountAsync();
    Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync();
    Task<ApiResponse<User>> GetUserByIdAsync(int id);
    Task<ApiResponse<object>> AddUserAsync(User user);
    Task<ApiResponse<object>> UpdateUserAsync(User user);
    Task<ApiResponse<object>> DeleteUserAsync(int id);
    Task<ApiResponse<bool>> UserExistsAsync(int id);
    Task<User> FetchAsync(string token);
    Task<ApiResponse<ProfileDto>> GetUserProfileAsync(int userId);
    Task<ApiResponse<User>> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<ApiResponse<LoginResponse>> LoginUserAsync(UserLoginDto userLoginDto);
    Task<bool> EmailExistsAsync(string email);
    Task<ApiResponse<object>> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
}

