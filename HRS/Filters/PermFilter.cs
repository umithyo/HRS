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
    public class PermissionAuthorize : ActionFilterAttribute, IActionFilter
    {
        public string Permissions { get; set; }
        private bool IsApi { get; set; }
        private string _unauthorizedRedirectUri;
        public string UnauthorizedRedirectUri {
            get {
                return string.IsNullOrEmpty(_unauthorizedRedirectUri) ? "/Home/Login" : _unauthorizedRedirectUri;
            }
            set {
                _unauthorizedRedirectUri = value;
            }
        }

        private ActionExecutingContext context;
        private string[] _Permissions { get; set; }

        public PermissionAuthorize() : base()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext _context)
        {
            context = _context;
            IsApi = ((ControllerActionDescriptor)context.ActionDescriptor)
                .ControllerTypeInfo
                .CustomAttributes
                .Any(x => x.AttributeType == typeof(ApiControllerAttribute));
            var sessionManager = context.HttpContext.RequestServices.GetService<ISessionManager>();
            var userRole = sessionManager.GetRole();
            if (Permissions != null)
            {
                _Permissions = Permissions.Replace(" ", "").Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (!_Permissions.Any(x => x == userRole))
                {
                    if (IsApi == true)
                        context.Result = new UnauthorizedResult();
                    else
                        Redirect(HttpStatusCode.Unauthorized, UnauthorizedRedirectUri);
                }
            }
            else
            {
                if (!sessionManager.IsLoggedIn())
                {
                    if (IsApi == true)
                        context.Result = new UnauthorizedResult();
                    else
                        Redirect(HttpStatusCode.Unauthorized, UnauthorizedRedirectUri);
                }
            }
            base.OnActionExecuting(context);
        }

        private void Redirect(HttpStatusCode code, string RedirectTo)
        {
            if (context != null)
            {
                context.Result = new RedirectResult(RedirectTo);
            }
        }
    }
}
