namespace TaiLieuWebsiteBackend.DTOs
{
    public class CommentCreateDto : CommentUpdateDto
    {
        public int? DocumentId { get; set; }
        public int? GameId { get; set; }
        public int? VideoId { get; set; }
        public int? ComicId { get; set; }
    }

    public class CommentUpdateDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
