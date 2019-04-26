using HRS.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static HRS.Helpers.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HRS.Filters
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class PermissionAuthorize : ActionFilterAttribute, IActionFilter
    {
        public string[] Permissions { get; set; }
        public string RedirectUri { get; set; }

        public PermissionAuthorize():base()
        {   
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionManager = context.HttpContext.RequestServices.GetService<ISessionManager>();
            var userRole = sessionManager.GetRole();
            if (Permissions != null)
            {
                if (!Permissions.Any(x => x == userRole))
                {
                    Redirect(context, RedirectUri);
                }
            }
            else
            {
                if (!sessionManager.IsLoggedIn())
                {
                    Redirect(context, RedirectUri);
                }
            }
        }    

        private void Redirect(ActionExecutingContext context, string RedirectUri)
        {
            if (context != null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.HttpContext.Response.Redirect(!string.IsNullOrEmpty(RedirectUri) ? RedirectUri : "/Home/Login");
            }
        }
    }
}
