using System.Collections.Generic;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Response;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Component;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace TaiLieuWebsiteBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly JwtTokenUtil _jwtTokenUtil;

        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasher, JwtTokenUtil jwtTokenUtil)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenUtil = jwtTokenUtil;
        }

        public async Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<ApiResponse<object>> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<ApiResponse<object>> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<ApiResponse<object>> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<ApiResponse<bool>> UserExistsAsync(int id)
        {
            return await _userRepository.UserExistsAsync(id);
        }

        public async Task<ApiResponse<User>> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var user = new User
            {
                username = userRegisterDto.Username,
                password_hash = _passwordHasher.HashPassword(userRegisterDto.Password),
                email = userRegisterDto.Email,
                role = userRegisterDto.Role
            };

            var response = await _userRepository.AddUserAsync(user);
            if (response.StatusCode == 201)
            {
                return ApiResponse<User>.Success(201, "Đăng ký người dùng thành công", user);
            }
            return ApiResponse<User>.Error(response.StatusCode, response.Message, response.ErrorMessage);
        }

        public async Task<ApiResponse<LoginResponse>> LoginUserAsync(UserLoginDto userLoginDto)
        {
            var response = await _userRepository.GetUserByUsernameOrEmailAsync(userLoginDto.UsernameOrEmail);
            if (response.StatusCode != 200 || !_passwordHasher.VerifyPassword(userLoginDto.Password, response.Data.password_hash))
            {
                return ApiResponse<LoginResponse>.Error(401, "Đăng nhập thất bại", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            var token = _jwtTokenUtil.GenerateToken(response.Data);

            var loginResponse = new LoginResponse(token, response.Data);
            return ApiResponse<LoginResponse>.Success(200, "Đăng nhập thành công", loginResponse);
        }

        public async Task<User> FetchAsync(string token)
        {
            var principal = _jwtTokenUtil.ValidateToken(token);
            if (principal == null)
            {
                return null;
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return null;
            }

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                var response = await _userRepository.GetUserByIdAsync(userId);
                if (response.StatusCode == 200)
                {
                    return response.Data;
                }
            }

            return null;
        }

        public async Task<ApiResponse<ProfileDto>> GetUserProfileAsync(int userId)
        {
            var response = await _userRepository.GetUserByIdAsync(userId);
            if (response.StatusCode != 200)
            {
                return ApiResponse<ProfileDto>.Error(404, "User not found", "Not Found");
            }

            var user = response.Data;
            var profile = new ProfileDto
            {
                Id = user.user_id,
                Username = user.username,
                Avatar = user.ProfilePicturePath,
                Role = user.role,
                Email = user.email
            };

            return ApiResponse<ProfileDto>.Success(200, "Profile retrieved successfully", profile);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<ApiResponse<object>> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            return await _userRepository.ChangePasswordAsync(userId, oldPassword, newPassword);
        }
        public async Task<ApiResponse<int>> GetUserCountAsync()
        {
            return await _userRepository.GetUserCountAsync();
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string keyword)
        {
            return await _userRepository.SearchUsersAsync(keyword);
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _userRepository.UsernameExistsAsync(username);
        }
    }
}

