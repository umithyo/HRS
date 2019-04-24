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
        void SetLoggedIn(User user);
        void SetLoggedOut();
        Guid? GetUserId();
        User GetUser();
    }

    public class SessionManager : ISessionManager
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

            _setRange(info);
        }

        public void SetLoggedOut()
        {
            var info = new Dictionary<string, string>
            {
                { "UserID", "" },
                { "Role", "" },
            };

            _setRange(info);
        }

        public Guid? GetUserId()
        {
            if (!IsLoggedIn())
                return null;
            return new Guid(_getString("UserID"));      
        }

        public User GetUser()
        {
            var u_id = GetUserId();
            if (u_id == null)
                return null;
            return context.Users.FirstOrDefault(x => x.Id == u_id);      
        }

        private void _setRange(Dictionary<string, string> info)
        {
            foreach (var item in info)
            {
                _setString(item.Key, item.Value);
            }
        }

        private void _setString(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        private string _getString(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}
