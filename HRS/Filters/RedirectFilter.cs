using HRS.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HRS.Filters
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RedirectFilter : ActionFilterAttribute, IActionFilter
    {
        public string Permissions { get; set; }
        public string AuthorizedRedirectUri { get; set; }

        private ActionExecutingContext context;
        private string[] _Permissions { get; set; }

        public RedirectFilter() : base()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext _context)
        {
            context = _context;
            var sessionManager = context.HttpContext.RequestServices.GetService<ISessionManager>();
            var userRole = sessionManager.GetRole();
            if (Permissions != null && sessionManager.IsLoggedIn())
            {
                _Permissions = Permissions.Replace(" ", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (_Permissions.Any(x => x == userRole))
                    Redirect(HttpStatusCode.Redirect, AuthorizedRedirectUri);
            }          
        }

        private void Redirect(HttpStatusCode code, string RedirectTo = "/Home/Login")
        {
            if (context != null)
            {
                context.HttpContext.Response.StatusCode = (int)code;
                context.HttpContext.Response.Redirect(RedirectTo);
            }
        }
    }
}
