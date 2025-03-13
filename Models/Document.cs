using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    public class Document
    {
        [Key]
        public int document_id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string title { get; set; } 

        public string description { get; set; } 

        [Required]
        public string file_path { get; set; }

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
        public ICollection<Comment> Comments { get; set; }

    }
}
