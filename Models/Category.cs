using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; } 

        [Required]
        [MaxLength(50)]
        public string name { get; set; } // Tên danh mục, kiểu VARCHAR(50)

        public string description { get; set; } // Mô tả về danh mục, kiểu TEXT

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Khóa ngoại đến bảng Categories
        [ForeignKey("Class")]
        public int class_id { get; set; }
        public Class Class { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
