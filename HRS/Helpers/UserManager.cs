using HRS.Data;
using HRS.Exceptions;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static HRS.Data.Constants;
using static HRS.Helpers.Utils;

namespace HRS.Helpers
{
    public interface IUserManager
    {
        UserInfo GetUserInfo(User user);
        User GetUser(Guid id);
        bool UserExists(Guid id);
        ManagerStatus Register(User user, UserInfo userinfo);
        ManagerStatus SignIn(User _user);
        ManagerStatus UpdateUser(User _user, UserInfo _userinfo);
        ManagerStatus RemoveUser(Guid id);
    }

    public class UserManager : IUserManager
    {
        IHttpContextAccessor accessor;
        private HttpContext HttpContext;
        private readonly ManagerContext context;
        private readonly ISessionManager sessionManager;

        public UserManager(IHttpContextAccessor _accessor, ManagerContext _context, ISessionManager _sessionManager)
        {
            accessor = _accessor;
            HttpContext = accessor.HttpContext;
            context = _context;
            sessionManager = _sessionManager;
        }

        public UserInfo GetUserInfo(User user)
        {
            return context.Users.Include(x => x.UserInfo).Where(x => x.Id == user.Id).Select(x=>x.UserInfo).FirstOrDefault();
        }

        public User GetUser(Guid id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserFromTC(string tc)
        {
            return context.Users.FirstOrDefault(x => x.TCKimlikNo == tc);
        }
       
        public bool UserExists(Guid id)
        {
            return context.Users.Any(x => x.Id == id);
        }

        public ManagerStatus Register(User user, UserInfo userInfo)
        {
            try
            {
                var exists = CheckUserInfoValidity(user, userInfo);
                if (exists.exists)
                    return exists.ManagerStatus;

                user.CreatedAt = DateTime.Now;
                userInfo.CreatedAt = DateTime.Now;

                user.Password = HashString.GetMD5(user.Password);
                user.UserInfo = userInfo;

                context.Add(user);
                context.Add(userInfo);
                context.SaveChanges();
                return ManagerStatus.OK;
            }
            catch (Exception)
            {
                return ManagerStatus.UNKNOWN;
            }
        }

        public ManagerStatus SignIn(User _user)
        {
            try
            {
                var user = GetUserFromTC(_user.TCKimlikNo);
                if (user == null)
                    return ManagerStatus.USER_NOT_FOUND;
                if (user.Password != HashString.GetMD5(_user.Password))
                    return ManagerStatus.USER_WRONG_CREDENTIALS;
                sessionManager.SetLoggedIn(user);
                return ManagerStatus.OK;
            }
            catch (Exception)
            {
                return ManagerStatus.UNKNOWN;
            }
        }

        public ManagerStatus UpdateUser(User _user, UserInfo _userinfo)
        {
            try
            {
                if (!UserExists(_user.Id))
                    return ManagerStatus.USER_NOT_FOUND;
                               
                if (_userinfo != null)
                {
                    context.Update(_userinfo);
                }
                context.Update(_user);

                context.SaveChanges();
                return ManagerStatus.OK;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                return ManagerStatus.UNKNOWN;
            }
        }

        public ManagerStatus RemoveUser(Guid id)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                    return ManagerStatus.NOT_FOUND;
                context.Remove(user);
                context.SaveChanges();
                return ManagerStatus.OK;
            }
            catch (Exception)
            {
                return ManagerStatus.UNKNOWN;
            }
        }

        private dynamic CheckUserInfoValidity(User user, UserInfo userInfo)
        {
            var e = new Dictionary<ManagerStatus, bool>() 
            {
                {ManagerStatus.USER_TC_EXISTS, context.Users.Any(x=>x.TCKimlikNo == user.TCKimlikNo) },
                {ManagerStatus.USER_EMAIL_EXISTS, context.UserInfos.Any(x=>x.Email == userInfo.Email) },
                {ManagerStatus.USER_PHONE_EXISTS, context.UserInfos.Any(x=>x.Phone == userInfo.Phone) }
            };

            foreach (var item in e)
            {
                if (item.Value)
                    return new { exists = true, ManagerStatus = item.Key };
            }

            return new { exists = false, ManagerStatus = ManagerStatus.OK };
        }
    }
}