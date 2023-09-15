using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using VF.Patika_W2.API.Service.User;

namespace VF.Patika_W2.API.Attributes.Auth
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userService = context.HttpContext.RequestServices.GetService<IUserService>();
            var username = context.HttpContext.Request.Headers["Username"].FirstOrDefault();
            var password = context.HttpContext.Request.Headers["Password"].FirstOrDefault();

            if (!userService.IsValidUser(username, password))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
