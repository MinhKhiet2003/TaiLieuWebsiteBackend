using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaiLieuWebsiteBackend.Models.TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Models
{
    public class Game
    {
        [Key]
        public int game_id { get; set; }

        public string game_url { get; set; } 
        public string description { get; set; }
        public string classify { get; set; }
        [Required]
        [MaxLength(5000)]
        public string title { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        // Khóa ngoại đến bảng Categories
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }

        // Khóa ngoại đến bảng Users
        [ForeignKey("User")]
        public int uploaded_by { get; set; }
        public User User { get; set; }

        public ICollection<Star> Stars { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
