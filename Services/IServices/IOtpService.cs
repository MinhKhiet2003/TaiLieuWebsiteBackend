namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IOtpService
    {
        string GenerateOtp();
        Task StoreOtpAsync(string email, string otp);
        Task<bool> VerifyOtpAsync(string email, string otp);
    }
}
