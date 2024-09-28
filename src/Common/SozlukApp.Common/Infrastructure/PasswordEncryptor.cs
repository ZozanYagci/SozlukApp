using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrpt(string password)
        {
            //using var md5= MD5.Create();
            using var sha256 = SHA256.Create();

            byte[] inputBytes=Encoding.UTF8.GetBytes(password);
            byte[] hashBytes= sha256.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
