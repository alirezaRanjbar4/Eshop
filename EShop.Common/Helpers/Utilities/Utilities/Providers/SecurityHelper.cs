using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class SecurityHelper
    {
        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "0123456789";
        const string SPECIALS = @"!@£$%^&*()#€";
        private byte[] defaultKey = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
        private byte[] defaultIV = Encoding.UTF8.GetBytes("0123456789ABCDEF");

        public string GeneratePassword(bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSpecial = true, int passwordSize = 12)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;
            if (useUppercase) charSet += UPPER_CASE;
            if (useNumbers) charSet += NUMBERS;
            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                // ensure we use at least 1 type of each subset of char
                if (counter == 0 && useLowercase)
                    _password[counter] = LOWER_CASE[_random.Next(LOWER_CASE.Length - 1)];
                else if (counter == 1 && useUppercase)
                    _password[counter] = UPPER_CASE[_random.Next(UPPER_CASE.Length - 1)];
                else if (counter == 2 && useNumbers)
                    _password[counter] = NUMBERS[_random.Next(NUMBERS.Length - 1)];
                else if (counter == 3 && useSpecial)
                    _password[counter] = SPECIALS[_random.Next(SPECIALS.Length - 1)];
                else
                    _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return string.Join(null, _password);
        }


        public byte[] Encrypt(string plainText, byte[] key = null, byte[] iv = null)
        {
            if (key == null)
                key = defaultKey;
            if (iv == null)
                iv = defaultIV;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        public string Decrypt(byte[] cipherText, byte[] key = null, byte[] iv = null)
        {
            if (key == null)
                key = defaultKey;
            if (iv == null)
                iv = defaultIV;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }



        public string CompressString(string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    gzipStream.Write(byteArray, 0, byteArray.Length);
                }
                return Convert.ToBase64String(memoryStream.ToArray()); // تبدیل بایت‌ها به Base64
            }
        }

    }
}
