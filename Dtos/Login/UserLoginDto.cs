using System.ComponentModel.DataAnnotations;

namespace TaiLieuWebsiteBackend.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(50)]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

