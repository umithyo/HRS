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
using Microsoft.AspNetCore.Mvc.Controllers;

namespace HRS.Filters
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RedirectFilter : ActionFilterAttribute, IActionFilter
    {
        public string Permissions { get; set; }
        public string AuthorizedRedirectUri { get; set; }
        public bool RedirectOnce { get; set; }

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
            var actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
           
            if (Permissions != null && sessionManager.IsLoggedIn())
            {
                _Permissions = Permissions.Replace(" ", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (_Permissions.Any(x => x == userRole))
                {
                    if (RedirectOnce == true)
                    {
                        var _redirected = sessionManager._getString("Redirected");
                        if (!string.IsNullOrEmpty(_redirected) && _redirected.Contains(actionName + ":" + AuthorizedRedirectUri))
                            return;
                        else
                            if (!string.IsNullOrEmpty(_redirected))
                            sessionManager._setString("Redirected", _redirected + ";" + (actionName + ":" + AuthorizedRedirectUri));
                        else
                            sessionManager._setString("Redirected", (actionName + ":" + AuthorizedRedirectUri));
                    }
                    Redirect(HttpStatusCode.Redirect, AuthorizedRedirectUri);
                }
               
            }          
        }

        private void Redirect(HttpStatusCode code, string RedirectTo = "/Home/Login")
        {
            if (context != null)
            {
                context.Result = new RedirectResult(RedirectTo);
            }
        }
    }
}
