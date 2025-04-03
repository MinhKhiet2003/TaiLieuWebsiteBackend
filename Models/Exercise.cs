using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    public class Exercise
    {
        [Key]
        public int exercise_id { get; set; } 

        [MaxLength(20)]
        public string difficulty { get; set; } // Mức độ khó ('easy', 'medium', 'hard'), kiểu VARCHAR(20)

        public string description { get; set; }
        [Required]
        [MaxLength(5000)]
        public string title { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Khóa ngoại đến bảng Categories
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }

        // Khóa ngoại đến bảng Users
        [ForeignKey("User")]
        public int uploaded_by { get; set; }
        public User User { get; set; }

        public ICollection<Star> Stars { get; set; }
    }
}
