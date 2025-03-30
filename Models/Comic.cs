using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaiLieuWebsiteBackend.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comic_url { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey("User")]
        public int Uploaded_by { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        // Khóa ngoại đến bảng Categories
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
