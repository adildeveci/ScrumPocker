using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace ScrumPocker.Core.Helpers
{
    public static class Hashing
    {
        private const string saltBase64 = "U8Sxa8SxbMSxeW9ydW1TYWJyaUJ1bmFsxLF5b3J1bTYhT0Y=";
        public static string HashSHA512(string input)
        {
            var salt = Convert.FromBase64String(saltBase64);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
            return Convert.ToBase64String(bytes);
        }
        public static bool CheckHashSHA512(string clearText, string hashKey)
        {
            try
            {
                var salt = Convert.FromBase64String(saltBase64);
                var bytes = KeyDerivation.Pbkdf2(clearText, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);
                return hashKey.Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }
    }
}
