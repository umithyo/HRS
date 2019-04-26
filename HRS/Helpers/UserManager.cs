using HRS.Data;
using HRS.Exceptions;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static HRS.Helpers.Utils;

namespace HRS.Helpers
{
    public interface IUserManager
    {

    }
    public class UserManager : IUserManager
    {
        IHttpContextAccessor accessor;
        private HttpContext HttpContext;
        private readonly ManagerContext context;

        public UserManager(IHttpContextAccessor _accessor, ManagerContext _context)
        {
            accessor = _accessor;
            HttpContext = accessor.HttpContext;
            context = _context;
        }

        public List<UserInfo> GetUserInfo(User user)
        {
            return context.UserInfos.Include(x => x.User).Where(x => x.User.Id == user.Id).ToList();
        }

        public void Register(User user, UserInfo userInfo)
        {
            try
            {
                var exists = UserExists(user, userInfo);
                if (exists.exists)
                    throw new RegistrationException(exists.message);

                user.CreatedAt = DateTime.Now;
                userInfo.CreatedAt = DateTime.Now;
                user.Role = RoleConfig.User;

                context.Add(user);
                context.Add(userInfo);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic UserExists(User user, UserInfo userInfo)
        {
            var expressions = new
            {
                tc = _exists<User>(x=>x.TCKimlikNo == user.TCKimlikNo),
                email = _exists<UserInfo>(x=>x.Email == userInfo.Email),
                phone = _exists<UserInfo>(x=>x.Phone == userInfo.Phone)
            };

            if (context.Users.Any(expressions.tc))
                return new { exists = true, message = "Bu TC kimlik numarası zaten kullanımda." };
            if (context.UserInfos.Any(expressions.email))
                return new { exists = true, message = "Bu email adresi zaten kullanımda." };
            if (context.UserInfos.Any(expressions.phone))
                return new { exists = true, message = "Bu telefon numarası zaten kullanımda." };

            return new { exists = false };
        }

        private Expression<Func<T, bool>> _exists<T>(Expression<Func<T, bool>> expression)
        {
            return expression;
        }
    }
}