using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Tools
{
    public class Security
    {
        private static byte[] GetKey()
        {
            int key_size = 32;
            byte[] key = new byte[key_size];
            string hwid = sha256($"{Environment.UserName}{Environment.MachineName}");

            for (int i = 0; i < key_size; i++)
            {
                if (hwid.Length > i + 1)
                {
                    key[i] = (byte)hwid[i];
                }
            }

            return (key);
        }

        public static byte[] GetPadding()
        {
            int padding_size = 16;
            byte[] padding = new byte[padding_size];
            string hwid = sha256($"{Environment.MachineName}{Environment.UserName}");

            for (int i = 0; i < padding_size; i++)
            {
                if (hwid.Length > i + 1)
                {
                    padding[i] = (byte)hwid[i] ^ i;
                }
            }

            return (padding);
        }

        static string sha256(string data)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(data));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte);
            }

            return hash;
        }


        public static byte[] Encrypt(byte[] plainBytes)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetKey();
                aesAlg.IV = GetPadding();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        public static byte[] Decrypt(byte[] cipherBytes)
        {
            byte[] decryptedBytes;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetKey();
                aesAlg.IV = GetPadding();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
                        {
                            csDecrypt.CopyTo(msOutput);
                            decryptedBytes = msOutput.ToArray();
                        }
                    }
                }
            }

            return decryptedBytes;
        }
    }
}
