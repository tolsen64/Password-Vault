using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoycoT_Password_Vault
{
    internal static class PasswordGenerator
    {
        private static readonly List<int> sym = new[] { 33, 35, 36, 37, 38, 42, 43, 45, 58, 59, 61, 63, 64 }.ToList();
        private static readonly List<int> num = new[] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 }.ToList();
        private static readonly List<int> up = new[] { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 }.ToList();
        private static readonly List<int> lo = new[] { 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 }.ToList();

        public static string CreatePassword(int length, bool symbols, bool numbers, bool upperCase, bool lowerCase)
        {
            string pwd = "";
            using (var rng = RandomNumberGenerator.Create())
            {
                while (pwd.Length < length)
                {
                    var b = new byte[1];
                    rng.GetBytes(b);
                    int i = b[0];
                    if (lowerCase && lo.Contains(i)) pwd = $"{pwd}{(char)i}";
                    else if (upperCase && up.Contains(i)) pwd = $"{pwd}{(char)i}";
                    else if (numbers && num.Contains(i)) pwd = $"{pwd}{(char)i}";
                    else if (symbols && sym.Contains(i)) pwd = $"{pwd}{(char)i}";
                }
                return pwd;
            }
        }
    }
}