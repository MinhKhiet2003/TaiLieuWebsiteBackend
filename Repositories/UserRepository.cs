using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Response;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasherService _passwordHasher;

        public UserRepository(AppDbContext context, IPasswordHasherService passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return ApiResponse<IEnumerable<User>>.Success(200, "Lấy danh sách người dùng thành công", users);
        }

        public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return ApiResponse<User>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với ID được cung cấp.");
            }
            return ApiResponse<User>.Success(200, "Lấy người dùng thành công", user);
        }

        public async Task<ApiResponse<object>> AddUserAsync(User user)
        {
            try
            {
                // Kiểm tra email đã tồn tại
                if (await EmailExistsAsync(user.email))
                {
                    return ApiResponse<object>.Error(400, "Email đã tồn tại", "Email đã được sử dụng bởi người dùng khác.");
                }

                // Chuyển mật khẩu thành hash
                user.password_hash = _passwordHasher.HashPassword(user.password_hash);

                ValidateUser(user);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return ApiResponse<object>.Success(201, "Thêm người dùng thành công", null);
            }
            catch (ValidationException ex)
            {
                return ApiResponse<object>.Error(400, "Lỗi xác thực", ex.Message);
            }
        }


        public async Task<ApiResponse<object>> UpdateUserAsync(User user)
        {
            try
            {
                var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.user_id == user.user_id);
                if (existingUser == null)
                {
                    return ApiResponse<object>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với ID được cung cấp.");
                }

                if (await _context.Users.AnyAsync(u => u.email == user.email && u.user_id != user.user_id))
                {
                    return ApiResponse<object>.Error(400, "Email đã tồn tại", "Email đã được sử dụng bởi người dùng khác.");
                }

                user.CreatedAt = existingUser.CreatedAt;

                TimeZoneInfo hanoiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                user.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hanoiTimeZone);

                if (user.password_hash != existingUser.password_hash)
                {
                    user.password_hash = _passwordHasher.HashPassword(user.password_hash);
                }

                ValidateUser(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return ApiResponse<object>.Success(200, "Cập nhật người dùng thành công", null);
            }
            catch (ValidationException ex)
            {
                return ApiResponse<object>.Error(400, "Lỗi xác thực", ex.Message);
            }
        }




        public async Task<ApiResponse<object>> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return ApiResponse<object>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với ID được cung cấp.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return ApiResponse<object>.Success(200, "Xóa người dùng thành công", null);
        }

        public async Task<ApiResponse<bool>> UserExistsAsync(int id)
        {
            var exists = await _context.Users.AnyAsync(e => e.user_id == id);
            return ApiResponse<bool>.Success(200, "Kiểm tra sự tồn tại của người dùng thành công", exists);
        }

        public async Task<ApiResponse<User>> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == usernameOrEmail || u.email == usernameOrEmail);
            if (user == null)
            {
                return ApiResponse<User>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với tên đăng nhập hoặc email được cung cấp.");
            }
            return ApiResponse<User>.Success(200, "Lấy người dùng thành công", user);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        private void ValidateUser(User user)
        {
            var validationContext = new ValidationContext(user);
            Validator.ValidateObject(user, validationContext, validateAllProperties: true);

            if (user.password_hash.Length < 6)
            {
                throw new ValidationException("Mật khẩu phải có ít nhất 6 ký tự.");
            }

            if (user.username.Length < 3)
            {
                throw new ValidationException("Tên đăng nhập phải có ít nhất 3 ký tự.");
            }
        }

        public async Task<ApiResponse<object>> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return ApiResponse<object>.Error(404, "User not found", "Not Found");
            }

            if (!_passwordHasher.VerifyPassword(oldPassword, user.password_hash))
            {
                return ApiResponse<object>.Error(400, "Old password is incorrect", "Bad Request");
            }

            user.password_hash = _passwordHasher.HashPassword(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return ApiResponse<object>.Success(204, "Password changed successfully", null);
        }
        public async Task<ApiResponse<int>> GetUserCountAsync()
        {
            int count = await _context.Users.CountAsync();
            return ApiResponse<int>.Success(200, "Lấy tổng số lượng người dùng thành công", count);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.username == username);
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string keyword)
        {
            return await _context.Users
                .Where(u => u.username.Contains(keyword) ||
                            u.email.Contains(keyword) ||
                            u.role.Contains(keyword))
                .ToListAsync();
        }

    }
}
