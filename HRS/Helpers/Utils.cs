using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Helpers
{
    public class Utils
    {
        public static class RoleConfig
        {
            public static readonly string Founder = "Developer";
            public static readonly string Admin = "Admin";
            public static readonly string Operator = "Operator";
            public static readonly string User = "User";
        }

        public static class HashString
        {
            public static string GetMD5(string text)
            {
                MD5 hash = MD5.Create();

                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                hash.Clear();

                return builder.ToString();
            }
        }
    }
}
