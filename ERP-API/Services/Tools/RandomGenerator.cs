using System;
using System.Security.Cryptography;
using System.Text;

namespace ERP_API.Services.Tools
{
    public class RandomGenerator
    {
        private static readonly char[] PasswordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private static readonly char[] TokenChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public string GenerateRandomPassword(int length = 10)
        {
            return GenerateRandomString(PasswordChars, length);
        }

        public string GenerateRandomToken(int length = 20)
        {
            return GenerateRandomString(TokenChars, length);
        }

        private string GenerateRandomString(char[] characterSet, int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be a positive number.");

            var randomString = new StringBuilder(length);
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    var randomIndex = randomBytes[i] % characterSet.Length;
                    randomString.Append(characterSet[randomIndex]);
                }
            }

            return randomString.ToString();
        }
    }
}
