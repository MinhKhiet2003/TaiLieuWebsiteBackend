using System.ComponentModel.DataAnnotations;

namespace TaiLieuWebsiteBackend.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
