﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoHelper
{
    public class Crypto
    {
        public static string Encrypt(string inputValue, string inputsalt)
        {

            string salt = "#20@20!%$" + inputsalt;
            byte[] utfData = UTF8Encoding.UTF8.GetBytes(inputValue);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            string encryptedString = string.Empty;

            using (AesManaged aes = new AesManaged())
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {
                    using (MemoryStream encryptedStream = new MemoryStream())
                    {
                        using (CryptoStream encryptor =
                        new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {
                            encryptor.Write(utfData, 0, utfData.Length);
                            encryptor.Flush();
                            encryptor.Close();
                            byte[] encryptBytes = encryptedStream.ToArray();
                            encryptedString = Convert.ToBase64String(encryptBytes);
                        }
                    }
                }

            }

            return encryptedString;
        }


        public static string Decrypt(string inputValue, string inputsalt)

        {

            string salt = "#20@20!%$" + inputsalt;
            byte[] encryptedBytes = Convert.FromBase64String(inputValue);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            string decryptedString = string.Empty;
            using (var aes = new AesManaged())

            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);
                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {
                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decryptor.Flush();
                        decryptor.Close();
                        byte[] decryptBytes = decryptedStream.ToArray();
                        decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                    }
                }
            }
            return decryptedString;
        }

    }
}
