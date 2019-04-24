using HRS.Data;
using HRS.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Helpers
{
    public interface ISessionManager
    {
        bool IsLoggedIn();
    }

    public class SessionManager:ISessionManager
    {
        IHttpContextAccessor accessor;
        private HttpContext HttpContext;
        private readonly ManagerContext context;

        public SessionManager(IHttpContextAccessor _accessor, ManagerContext _context)
        {
            accessor = _accessor;
            HttpContext = accessor.HttpContext;
            context = _context;
        }

        public bool IsLoggedIn()
        {
            return !String.IsNullOrEmpty(HttpContext.Session.GetString("Role"));
        }

        public void SetLoggedIn(User user)
        {
            var info = new Dictionary<string, string>
            {
                { "UserID", user.Id.ToString() },
                { "Role", user.Role },
            };
        }
    }
}
