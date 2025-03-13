﻿namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        string GenerateSalt();
    }

}
