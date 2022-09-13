using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using coreWeb.Models.Entities;

using coreWeb.Services;
using Hangfire.Dashboard;

namespace coreWeb.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class DbAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _someFilterParameter;
        private ApplicationDbContext _context;
        public DbAuthorizeAttribute(string someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
        }
        public DbAuthorizeAttribute(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbAuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userContext = context.HttpContext.User;

            if (!userContext.Identity.IsAuthenticated)
            {
                return;
            }
            var user = new UserClaim(context.HttpContext);
            var path = context.HttpContext.Request.Path;
            // you can also use registered services

            var PermissionService = new PermissionService();
            var perObj = PermissionService.GetPermission(user.UserId, path);

            var isAuthorized = perObj != null;//= someService.IsUserAuthorized(user.Identity.Name, _someFilterParameter);
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                var cookies = httpContext.Request.Cookies;
                if (cookies["UToken"] != null && cookies["UUser"] =="admin@vnpt.vn")
                {
                    return true;
                }
                return false;
            }
        }
    }
}
