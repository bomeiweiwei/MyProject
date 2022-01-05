using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AllShowRepository.Utility
{
    public static class PasswordUtility
    {
        public static string AESEncryptor(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] data = ASCIIEncoding.ASCII.GetBytes(plainText);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            string encryptedString = Convert.ToBase64String(aes.CreateEncryptor(Key, IV).TransformFinalBlock(data, 0, data.Length));
            return encryptedString;
        }

        public static string AESDecryptor(string encryptedString, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (encryptedString == null || encryptedString.Length <= 0)
                throw new ArgumentNullException("encryptedString");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] data = Convert.FromBase64String(encryptedString);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            string decryptedString=ASCIIEncoding.ASCII.GetString(aes.CreateDecryptor(Key,IV).TransformFinalBlock(data,0,data.Length));
            return decryptedString;
        }
    }
}
