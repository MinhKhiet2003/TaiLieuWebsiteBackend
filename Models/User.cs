using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; } // Mã định danh duy nhất của người dùng, kiểu INT

        [Required]
        [MaxLength(100)]
        public string username { get; set; } // Tên người dùng, kiểu VARCHAR(50)

        [Required]
        public string password_hash { get; set; } // Mật khẩu được mã hóa, kiểu VARCHAR(255)

        [EmailAddress]
        public string email { get; set; } // Địa chỉ email của người dùng, kiểu VARCHAR(100)

        [Required]
        [MaxLength(20)]
        public string role { get; set; } = "user"; // Vai trò người dùng ('teacher', 'user', 'admin'), kiểu VARCHAR(20)

        public string? ProfilePicturePath { get; set; } // Đường dẫn tới ảnh đại diện của người dùng, kiểu VARCHAR(255)
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Star> Stars { get; set; }

    }
}
