namespace TaiLieuWebsiteBackend.Dtos
{
    public class CreateUpdateLifeDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Uploaded_by { get; set; }
        public int Category_id { get; set; }
    }
    public class LifeDto : CreateUpdateLifeDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt{ get; set; }
    }

}
