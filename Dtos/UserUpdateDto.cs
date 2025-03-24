using System.ComponentModel.DataAnnotations;

namespace TaiLieuWebsiteBackend.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? ProfilePicturePath { get; set; }
    }
}
