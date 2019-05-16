using HRS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Data
{
    public static class Constants
    {
        public static class RoleConfig
        {
            public const string Admin = "Yönetici";
            public const string Doctor = "Doktor";
            public const string Operator = "Operatör";
            public const string User = "Kullanıcı";

            public static string[] Roles = new string[] {
                Admin,
                Doctor,
                Operator,
                User
            };
        }
    }

    public enum ManagerStatus
    {
        OK,
        UNKNOWN,
        USER_NOT_FOUND,
        USER_TC_EXISTS,
        USER_EMAIL_EXISTS,
        USER_PHONE_EXISTS,
        USER_WRONG_CREDENTIALS,
        NOT_FOUND,
        EXISTS
    };

   
}
