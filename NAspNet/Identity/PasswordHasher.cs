using Microsoft.AspNetCore.Identity;

namespace NAspNet.Identity
{
    public static class PasswordHasher
    {
        private static readonly PasswordHasher<object> Hasher = new PasswordHasher<object>();

        public static string HashPassword(string password) => Hasher.HashPassword(null, password);
        public static PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword) => Hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);

    }
}
