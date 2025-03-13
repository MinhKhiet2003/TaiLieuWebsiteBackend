namespace TaiLieuWebsiteBackend.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
