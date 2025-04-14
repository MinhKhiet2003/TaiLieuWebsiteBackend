namespace TaiLieuWebsiteBackend.Dtos
{
    public class CreateUpdateDocumentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string file_path { get; set; }
        public int CategoryId { get; set; }
        public int UploadedBy { get; set; }
    }

    public class DocumentDto : CreateUpdateDocumentDto
    {
        public int Id { get; set; }
        public string UploadedByUsername { get; set; }
        public int CommentCount { get; set; }
        public double AverageRating { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
