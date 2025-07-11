using System;
using System.Security.Cryptography;
using System.Text;
using tripath.Services;
using tripath.Utils;

namespace tripath.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashWithMD5(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input), $"{AppStrings.Messages.PwdNullEmpty}");

            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
        }
    }
}