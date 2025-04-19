using Microsoft.Extensions.Caching.Memory;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _otpExpiration = TimeSpan.FromMinutes(5);

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task StoreOtpAsync(string email, string otp)
        {
            _cache.Set(email, otp, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _otpExpiration
            });
            await Task.CompletedTask;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            if (_cache.TryGetValue(email, out string storedOtp))
            {
                return await Task.FromResult(storedOtp == otp);
            }
            return await Task.FromResult(false);
        }
    }
}
