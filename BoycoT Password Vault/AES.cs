using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BoycoT_Password_Vault
{
    internal static class DES
    {
        internal static string Encrypt(string publickey, string privatekey, string text)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(Encoding.UTF8.GetBytes(GetKey(publickey, true)), Encoding.UTF8.GetBytes(GetKey(privatekey, false))), CryptoStreamMode.Write);
                    byte[] inputbyteArray = Encoding.UTF8.GetBytes(text);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        internal static string Decrypt(string publickey, string privatekey, string text)
        {
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(Encoding.UTF8.GetBytes(GetKey(publickey, true)), Encoding.UTF8.GetBytes(GetKey(privatekey, false))), CryptoStreamMode.Write);
                    byte[] inputbyteArray = Convert.FromBase64String(text.Replace(" ", "+"));
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private static string GetKey(string key, bool flag)
        {
            string h = $"{key}{flag}".ToHash();
            if (flag) return h.Substring(0, 8);
            return h.Substring(h.Length - 8, 8);
        }

        private static string ToHash(this string str)
        {
            using (SHA512Managed sha = new SHA512Managed())
            {
                return BitConverter.ToString(
                    sha.ComputeHash(
                        Encoding.UTF8.GetBytes(str)
                    )
                ).Replace("-", "");
            }
        }
    }
}
