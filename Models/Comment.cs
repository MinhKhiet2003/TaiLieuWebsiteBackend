using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    public class Comment
    {
        [Key]
        public int comment_id { get; set; } 

        [Required]
        public string content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Khóa ngoại đến bảng Documents
        [ForeignKey("Document")]
        public int document_id { get; set; }
        public Document Document { get; set; }

        // Khóa ngoại đến bảng Games
        [ForeignKey("Game")]
        public int game_id { get; set; }
        public Game Game { get; set; }

        // Khóa ngoại đến bảng Videos
        [ForeignKey("Video")]
        public int video_id { get; set; }
        public Video Video { get; set; }


        // Khóa ngoại đến bảng Users
        [ForeignKey("User")]
        public int user_id { get; set; }
        public User User { get; set; }
    }
}
