using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using NaijaHandyManAPI.Models;


namespace NaijaHandyManAPI.Controllers
{
    public class LogInController : ApiController
    {
        #region Encryption and Decryption Methods

        private HandyManEntities1 db = new HandyManEntities1();
        private string stringEncryptionKey = "TangoQuando".PadRight(32);
        private byte[] InitializationVector = new byte[] { 2, 4, 6, 8, 10, 12, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        //this method is used to encrypt a registering users password
        //before storing it in the database
        private string Encrypt(string RawPassword)
        {
            byte[] BytesToEncrypt = Encoding.UTF8.GetBytes(RawPassword);
            byte[] EncryptionKey = Encoding.UTF8.GetBytes(stringEncryptionKey);
            string EncryptedPassword = null;
            Aes AESAlgorithm = Aes.Create();
            AESAlgorithm.Key = EncryptionKey;
            AESAlgorithm.IV = InitializationVector;

            using (MemoryStream StreamIntoMemory = new MemoryStream())
            {
                using (CryptoStream EncryptionStream = new CryptoStream(StreamIntoMemory, AESAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    EncryptionStream.Write(BytesToEncrypt, 0, BytesToEncrypt.Length);
                }

                byte[] EncryptedBytes = StreamIntoMemory.ToArray();
                EncryptedPassword = Convert.ToBase64String(EncryptedBytes);
            }

            return EncryptedPassword;

        }

        private string Decrypt(string EncryptedPassword)
        {
            byte[] BytesToDecrypt = Convert.FromBase64String(EncryptedPassword);
            byte[] EncryptionKey = Encoding.UTF8.GetBytes(stringEncryptionKey);
            string RawPassword = null;
            Aes AESAlgorithm = Aes.Create();
            AESAlgorithm.Key = EncryptionKey;
            AESAlgorithm.IV = InitializationVector;
            using (MemoryStream StreamIntoMemory = new MemoryStream())
            {
                using (CryptoStream DecryptionStream = new CryptoStream(StreamIntoMemory, AESAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    DecryptionStream.Write(BytesToDecrypt, 0, BytesToDecrypt.Length);
                }
                byte[] DecryptedBytes = StreamIntoMemory.ToArray();
                string DecryptedString = Encoding.UTF8.GetString(DecryptedBytes);
                RawPassword = DecryptedString;
            }

            return RawPassword;
        }

        #endregion
        
        // api/Login/
        [HttpPost]
        public User AuthenticateUser(User user)
        {
            var query = from c in db.Users
                        where ((c.UserName == user.UserName) || (c.Email == user.Email))
                        select c;
            List<User> Users = query.ToList<User>();

            if (Users == null || Users.Count == 0)
                return new User();
            if (Decrypt(Users[0].Password) == user.Password)
                return Users[0];
            else
                return new User();
        }

        
        //// api/logIn
        //public List<User> Get()
        //{
        //    var query = from c in db.Users
        //                select c;
        //    return query.ToList<User>();
        //}
    }
}
