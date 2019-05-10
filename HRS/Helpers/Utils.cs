using HRS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRS.Helpers
{
    public static class Utils
    {       
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

        public static string GetErrorString(ManagerStatus status)
        {
            switch (status)
            {
                case ManagerStatus.UNKNOWN:
                    return "Bilinmeyen bir hata oluştu.";
                case ManagerStatus.USER_NOT_FOUND:
                    return "Böyle bir kullanıcı bulunamadı";
                case ManagerStatus.USER_TC_EXISTS:
                    return "Bu TC Kimlik No ile başka bir kullanıcı zaten kayıtlı.";
                case ManagerStatus.USER_EMAIL_EXISTS:
                    return "Bu email ile başka bir kullanıcı zaten kayıtlı.";
                case ManagerStatus.USER_PHONE_EXISTS:
                    return "Bu telefon numarası ile başka bir kullanıcı zaten kayıtlı.";
                case ManagerStatus.USER_WRONG_CREDENTIALS:
                    return "Giriş bilgileri yanlış, lütfen doğru TC Kimlik NO ve şifre girdiğinizden emin olun.";
                case ManagerStatus.NOT_FOUND:
                    return "Aradığınız öğe bulunamadı.";
                case ManagerStatus.EXISTS:
                    return "Böyle bir öğe zaten mevcut.";
                default:
                    return "";
            }
        }
    }
}
