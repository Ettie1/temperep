using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace apifortemptes.Lib
{
    public class Security
    {
        public static string GetSalt(int size)
        {
            byte[] buff = new byte[size];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static byte[] Hash(string password, string salt)
        {
            byte[] _password = Encoding.UTF8.GetBytes(password);
            byte[] _salt = Encoding.UTF8.GetBytes(salt);
            byte[] saltedPw = _password.Concat(_salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedPw);
        }

        public static bool ConfirmPassword(string password, string passwdInDb, string salt)
        {
            byte[] encPasswd = Hash(password, salt);
            return Encoding.UTF8.GetBytes(passwdInDb).SequenceEqual(encPasswd);
        }
    }
}