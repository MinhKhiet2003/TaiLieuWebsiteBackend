namespace TaiLieuWebsiteBackend.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string file_path { get; set; }
        public int CategoryId { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
