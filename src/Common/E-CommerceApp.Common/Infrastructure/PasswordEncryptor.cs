using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashed = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashed);
        }
    }
}
