using System.ComponentModel.DataAnnotations;

namespace TaiLieuWebsiteBackend.DTOs
{
    public class StarCreateDto : StarUpdateDto
    {
        public int? DocumentId { get; set; }
        public int? ExerciseId { get; set; }
        public int? GameId { get; set; }
        public int? VideoId { get; set; }
        public int? ComicId { get; set; }
    }

    public class StarUpdateDto
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}