using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTL.Models.Authentication
{
    public class AuthenticationProfile : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("ManagerLogin") == null && context.HttpContext.Session.GetString("UserNameLogin") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Home" },
                        {"Action", "Login" }
                    });
            }
        }
    }
}
