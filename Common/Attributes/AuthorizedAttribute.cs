using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Saaya.Web.Utility;
using Saaya.Web.Db;
using Saaya.Web.Common.Extensions;

namespace Saaya.Web.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizedAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }

        public AuthorizedAttribute(string role = null)
        {
            Role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Session.GetUInt64("UserId");

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("github", "account", null);
                return;
            }

            var _db = context.HttpContext.RequestServices.GetRequiredService(typeof(SaayaWebContext)) as SaayaWebContext;

            var user = _db.Users.FirstOrDefault(x => x.Snowflake == userId);

            if (user == null)
            {
                if (Role == StaticDetails.Admin)
                {
                    context.Result = new RedirectToActionResult("index", "home", null);
                    return;
                }

                context.Result = new RedirectToActionResult("unauthorized", "account", null);
                return;
            }

            if (!HasRole(user.Role, Role))
            {
                context.Result = new RedirectToActionResult("index", "home", null);
                return;
            }
        }

        private bool HasRole(string roles, string roleName)
        {
            if (string.IsNullOrEmpty(roles)) return false;

            return roles.Split(',').Contains(roleName);
        }
    }
}