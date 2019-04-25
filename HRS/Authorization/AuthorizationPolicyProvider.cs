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

namespace HRS.Authorization
{

    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(PermissionAuthorizeAttribute.PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return base.GetPolicyAsync(policyName);
            }

            var permissionNames = policyName.Substring(PermissionAuthorizeAttribute.PolicyPrefix.Length).Split(',');

            var policy = new AuthorizationPolicyBuilder().RequireRole(permissionNames)
                //.RequireClaim(ClaimTypes..Permission, permissionNames)
                .Build();

            return Task.FromResult(policy);
        }
    }

    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION:";
        /// <summary>
        /// Creates a new instance of <see cref="AuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            //Roles =
            Policy = $"{PolicyPrefix}{string.Join(",", permissions)}";
        }
    }
}
