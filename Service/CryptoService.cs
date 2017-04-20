using System;
using System.Security.Cryptography;
using System.Text;
using EventLog.Service.Security;

namespace EventLog.Service
{
    public class CryptoService
    {
        private static string Salt = "mis polainas!";

        public string EncryptTextAes(string text)
        {
            return Crypto.EncryptStringAES(text, Salt);
        }

        public string DecryptTextAes(string text)
        {
            return Crypto.DecryptStringAES(text, Salt);
        }

        public string GetSHA256(string text)
        {
            string hash = "";
            // SHA512 is disposable by inheritance.  
            using(var sha256 = SHA256.Create())  
            {  
                // Send a sample text to hash.  
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));  
                // Get the hashed string.  
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();  
            }          

            return hash;    
        }
    }
}