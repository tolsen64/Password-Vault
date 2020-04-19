using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Security;
using System.Net;
using System.Text;

static class RijndaelAES
{

    internal static SecureString ToSecureString(this string plainText)
    {
        return new NetworkCredential("", plainText).SecurePassword;
    }

    internal static string ToUnsecureString(this SecureString secureString)
    {
        return new NetworkCredential("", secureString).Password;
    }

    internal static SecureString ToHash(this SecureString secureString)
    {
        using (SHA512Managed sha = new SHA512Managed())
        {
            return BitConverter.ToString(
                sha.ComputeHash(
                    Encoding.UTF8.GetBytes(
                        secureString.ToUnsecureString()
                    )
                )
            ).Replace("-", "").ToSecureString();
        }

    }

    internal static string EncryptTextToBase64String(this SecureString plainText)
    {
        byte[] IV = null;
        byte[] Key = null;
        using (RijndaelManaged rm = new RijndaelManaged())
        {
            rm.GenerateIV(); IV = rm.IV;
            rm.GenerateKey(); Key = rm.Key;
        }
        byte[] Str = EncryptStringToBytes(plainText, Key, IV);
        byte[] IVCount = BitConverter.GetBytes(IV.Count());
        byte[] KeyCount = BitConverter.GetBytes(Key.Count());
        byte[] StrCount = BitConverter.GetBytes(Str.Count());
        byte[] bytesOut = null;
        using (MemoryStream fs = new MemoryStream())
        {
            fs.Write(IVCount, 0, IVCount.Count());
            fs.Write(KeyCount, 0, KeyCount.Count());
            fs.Write(StrCount, 0, StrCount.Count());
            fs.Write(IV, 0, IV.Count());
            fs.Write(Key, 0, Key.Count());
            fs.Write(Str, 0, Str.Count());
            bytesOut = fs.ToArray();
            fs.Close();
        }
        return Convert.ToBase64String(bytesOut);
    }

    internal static byte[] EncryptStringToBytes(SecureString secureString, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (secureString == null || secureString.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        byte[] encrypted;
        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        // Write all data to the stream.
                        swEncrypt.Write(secureString.ToUnsecureString());
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }


        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    internal static SecureString DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        return srDecrypt.ReadToEnd().ToSecureString();
                    }
                }
            }
        }
    }

    internal static SecureString DecryptBase64StringToText(this string Base64String)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(Base64String);
            string HexString = BitConverter.ToString(bytes).Replace("-", "");

            byte[] IVCountHex = new byte[5];
            for (int i = 0; i <= 3; i++)
            {
                IVCountHex[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            byte[] KeyCountHex = new byte[5];
            for (int i = 0; i <= 3; i++)
            {
                KeyCountHex[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            byte[] StrCountHex = new byte[5];
            for (int i = 0; i <= 3; i++)
            {
                StrCountHex[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            int IVCount = BitConverter.ToInt32(IVCountHex, 0);
            byte[] IV = new byte[IVCount - 1 + 1];
            for (int i = 0; i <= IVCount - 1; i++)
            {
                IV[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            int KeyCount = BitConverter.ToInt32(KeyCountHex, 0);
            byte[] Key = new byte[KeyCount - 1 + 1];
            for (int i = 0; i <= KeyCount - 1; i++)
            {
                Key[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            int StrCount = BitConverter.ToInt32(StrCountHex, 0);
            byte[] Str = new byte[StrCount - 1 + 1];
            for (int i = 0; i <= StrCount - 1; i++)
            {
                Str[i] = Convert.ToByte(HexString.Substring(0, 2), 16);
                HexString = HexString.Substring(2);
            }
            return DecryptStringFromBytes(Str, Key, IV);
        }
        catch (Exception)
        {
            string msg = "Unable to decrypt";
            MessageBox.Show("Unable to decrypt.");
            SecureString s = new SecureString();
            foreach (char c in msg)
                s.AppendChar(c);
            return s;
        }
    }
}
