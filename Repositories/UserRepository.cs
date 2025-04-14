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
            var users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
            return ApiResponse<IEnumerable<User>>.Success(200, "Lấy danh sách người dùng thành công", users);
        }

        public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.user_id == id && !u.IsDeleted);
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
                // Kiểm tra username và email tồn tại
                if (await UsernameExistsAsync(user.username))
                {
                    return ApiResponse<object>.Error(400, "Username đã tồn tại", "Username đã được sử dụng bởi người dùng khác.");
                }

                if (await EmailExistsAsync(user.email))
                {
                    return ApiResponse<object>.Error(400, "Email đã tồn tại", "Email đã được sử dụng bởi người dùng khác.");
                }

                // Đặt giá trị mặc định
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                user.IsDeleted = false;

                ValidateUser(user);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return ApiResponse<object>.Success(201, "Thêm người dùng thành công", user);
            }
            catch (ValidationException ex)
            {
                return ApiResponse<object>.Error(400, "Lỗi xác thực", ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return ApiResponse<object>.Error(500, "Lỗi cơ sở dữ liệu", ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Error(500, "Lỗi hệ thống", ex.Message);
            }
        }


        public async Task<ApiResponse<object>> UpdateUserAsync(User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.user_id);
                if (existingUser == null)
                {
                    return ApiResponse<object>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với ID được cung cấp.");
                }

                // Kiểm tra username trùng (bỏ qua chính nó)
                if (existingUser.username != user.username &&
                    await UsernameExistsAsync(user.username))
                {
                    return ApiResponse<object>.Error(400, "Username đã tồn tại", "Username đã được sử dụng bởi người dùng khác.");
                }

                // Kiểm tra email trùng (bỏ qua chính nó)
                if (existingUser.email != user.email &&
                    await EmailExistsAsync(user.email))
                {
                    return ApiResponse<object>.Error(400, "Email đã tồn tại", "Email đã được sử dụng bởi người dùng khác.");
                }

                // Cập nhật các trường
                existingUser.username = user.username;
                existingUser.email = user.email;
                existingUser.role = user.role;
                existingUser.ProfilePicturePath = user.ProfilePicturePath;
                existingUser.UpdatedAt = DateTime.Now;

                // Chỉ cập nhật password nếu có thay đổi và không rỗng
                if (!string.IsNullOrEmpty(user.password_hash))
                {
                    existingUser.password_hash = _passwordHasher.HashPassword(user.password_hash);
                }

                ValidateUser(existingUser);
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return ApiResponse<object>.Success(200, "Cập nhật người dùng thành công", null);
            }
            catch (ValidationException ex)
            {
                return ApiResponse<object>.Error(400, "Lỗi xác thực", ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return ApiResponse<object>.Error(500, "Lỗi cơ sở dữ liệu", ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<ApiResponse<object>> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsDeleted)
            {
                return ApiResponse<object>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với ID được cung cấp.");
            }

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return ApiResponse<object>.Success(200, "Xóa người dùng thành công", null);
        }

        public async Task<ApiResponse<bool>> UserExistsAsync(int id)
        {
            var exists = await _context.Users.AnyAsync(e => e.user_id == id && !e.IsDeleted);
            return ApiResponse<bool>.Success(200, "Kiểm tra sự tồn tại của người dùng thành công", exists);
        }

        public async Task<ApiResponse<User>> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (u.username == usernameOrEmail || u.email == usernameOrEmail) && !u.IsDeleted);
            if (user == null)
            {
                return ApiResponse<User>.Error(404, "Không tìm thấy người dùng", "Không có người dùng nào với tên đăng nhập hoặc email được cung cấp.");
            }
            return ApiResponse<User>.Success(200, "Lấy người dùng thành công", user);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email && !u.IsDeleted);
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
            int count = await _context.Users.CountAsync(u => !u.IsDeleted);
            return ApiResponse<int>.Success(200, "Lấy tổng số lượng người dùng thành công", count);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.username == username && !u.IsDeleted);
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string keyword)
        {
            return await _context.Users
               .Where(u => !u.IsDeleted &&
                   (u.username.Contains(keyword) ||
                    u.email.Contains(keyword) ||
                    u.role.Contains(keyword)))
                    .ToListAsync();
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.username == username && !u.IsDeleted);
        }

    }
}
