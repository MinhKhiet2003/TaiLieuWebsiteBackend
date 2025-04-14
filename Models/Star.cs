using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaiLieuWebsiteBackend.Models
{
    namespace TaiLieuWebsiteBackend.Models
    {
        public class Star
        {
            [Key]
            public int star_id { get; set; }
            [Range(0, 5)]
            public int total_star { get; set; }
            [Required]
            public string ContentType { get; set; }

            [ForeignKey("Document")]
            public int? document_id { get; set; }
            public Document Document { get; set; }

            [ForeignKey("Exercise")]
            public int? exercise_id { get; set; }
            public Exercise Exercise { get; set; }

            [ForeignKey("Game")]
            public int? game_id { get; set; }
            public Game Game { get; set; }

            [ForeignKey("Video")]
            public int? video_id { get; set; }
            public Video Video { get; set; }

            [ForeignKey("Comic")]
            public int? comic_id { get; set; }
            public Comic Comic { get; set; }

            [ForeignKey("User")]
            public int user_id { get; set; }
            public User User { get; set; }
        }
    }
}
