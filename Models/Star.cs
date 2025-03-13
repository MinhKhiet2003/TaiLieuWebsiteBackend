using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    public class Star
    {
        [Key]
        public int star_id { get; set; }
        public int total_star { get; set; }
        [ForeignKey("Document")]
        public int document_id { get; set; }
        public Document Document { get; set; }

        // Khóa ngoại đến bảng Exercises
        [ForeignKey("Exercise")]
        public int exercise_id { get; set; }
        public Exercise Exercise { get; set; }

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
