using HRS.Data;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
