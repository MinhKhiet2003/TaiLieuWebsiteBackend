using System.ComponentModel.DataAnnotations;

namespace TaiLieuWebsiteBackend.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
