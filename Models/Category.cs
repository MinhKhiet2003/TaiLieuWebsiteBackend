using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Class Class { get; set; }

        // Khóa ngoại đến bảng Users
        [ForeignKey("User")]
        public int uploaded_by { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public ICollection<Document> Documents { get; set; }
        [JsonIgnore]
        public ICollection<Video> Videos { get; set; }
        [JsonIgnore]
        public ICollection<Exercise> Exercises { get; set; }
        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
        [JsonIgnore]
        public ICollection<Comic> Comics { get; set; }
        [JsonIgnore]
        public ICollection<Life> Lifes { get; set; }
    }
}
