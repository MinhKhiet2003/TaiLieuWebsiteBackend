namespace TaiLieuWebsiteBackend.DTOs
{
    public class CreateUpdateGameDto
    {
        public string title { get; set; }
        public string gameUrl { get; set; }
        public string description { get; set; }
        public int category_id { get; set; }
        public int uploaded_by { get; set; }
    }
    public class GameDto : CreateUpdateGameDto
    {
        public int Id { get; set; }
        public string UploadedByUsername { get; set; }
        public string category_name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
