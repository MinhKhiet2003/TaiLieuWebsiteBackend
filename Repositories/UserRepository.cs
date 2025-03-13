using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Response;

namespace TaiLieuWebsiteBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
