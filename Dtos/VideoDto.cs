namespace TaiLieuWebsiteBackend.Models
{
    public class CreateUpdateVideoDto
    {
        public string title { get; set; }
        public string video_url { get; set; }
        public string description { get; set; }
        public int category_id { get; set; }
        public int uploaded_by { get; set; }
    }
    public class VideoDto : CreateUpdateVideoDto
    {
        public int video_id { get; set; }
        
        public  string UploadedByUsername { get; set; }
        public string category_name { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
