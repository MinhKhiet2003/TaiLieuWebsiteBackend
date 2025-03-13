using TaiLieuWebsiteBackend.Models;

namespace TaiLieuWebsiteBackend.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public User User { get; set; }

        // Constructor có tham số
        public LoginResponse(string token, User user)
        {
            Token = token;
            User = user;
        }
    }
}
