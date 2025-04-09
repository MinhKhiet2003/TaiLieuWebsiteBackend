namespace TaiLieuWebsiteBackend.Dtos
{
    public class CreateUpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClassId { get; set; }
        public int UploadedBy { get; set; }
    }
    public class CategoryDto : CreateUpdateCategoryDto
    {
        public int Id { get; set; }
        public string UploadedByUsername { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CategorySimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
