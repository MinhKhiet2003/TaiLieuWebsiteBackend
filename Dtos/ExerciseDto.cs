namespace TaiLieuWebsiteBackend.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string difficulty { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int category_id { get; set; }
        public int UploadedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
