using Konscious.Security.Cryptography;
using Sahibinden.Business.Model.User;
using Sahibinden.Model.User;
using System.Text;

namespace Sahibinden.Business.Concrete.Services
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            int iterations = 4;
            int memorySize = 65536;
            int parallelism = 8;
            using (var argon2 = new Argon2id(passwordBytes))
            {
                argon2.Iterations = iterations;
                argon2.MemorySize = memorySize;
                argon2.DegreeOfParallelism = parallelism;
                byte[] salt = GenerateSalt();
                argon2.Salt = salt;
                byte[] hash = argon2.GetBytes(32);
                string hashedPasswword = Convert.ToBase64String(hash);
                string base64Salt = Convert.ToBase64String(salt);
                return $"$argon2id$iterations={iterations}$memory={memorySize}$parallelism={parallelism}${base64Salt}${hashedPasswword}";

            }
        }
        private static byte[] GenerateSalt()
        {
            var salt = new byte[32];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static bool VerifyPassword(UserLoginDetailModel userLoginDetailModel, string storedPassword)
        {
            var parts = storedPassword.Split('$');

            if (parts.Length < 6) return false;

            var iterations = int.Parse(parts[2].AsSpan(parts[2].IndexOf('=') + 1));
            var memorySize = int.Parse(parts[3].AsSpan(parts[3].IndexOf('=') + 1));
            var parallelism = int.Parse(parts[4].AsSpan(parts[4].IndexOf('=') + 1));
            var salt = parts[5];
            var storedSalt = Convert.FromBase64String(salt);
            var storedHash = parts[6];

            byte[] passwordBytes = Encoding.UTF8.GetBytes(userLoginDetailModel.Password);

            using (var argon2 = new Argon2id(passwordBytes))
            {
                argon2.Iterations = iterations;
                argon2.MemorySize = memorySize;
                argon2.DegreeOfParallelism = parallelism;
                argon2.Salt = storedSalt;

                byte[] hash = argon2.GetBytes(32);
                string hashBase64 = Convert.ToBase64String(hash);
                return hashBase64 == storedHash;
            }
        }
    }
}