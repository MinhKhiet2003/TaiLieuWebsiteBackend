namespace TaiLieuWebsiteBackend.Dtos
{
    public class CreateUpdateComicDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comic_url { get; set; }
        public int Uploaded_by { get; set; }
        public int Category_id { get; set; }
    }
    public class ComicDto : CreateUpdateComicDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
