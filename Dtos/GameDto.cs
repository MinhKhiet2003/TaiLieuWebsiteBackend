namespace TaiLieuWebsiteBackend.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GameUrl { get; set; }
        public int CategoryId { get; set; }
        public int UploadedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
