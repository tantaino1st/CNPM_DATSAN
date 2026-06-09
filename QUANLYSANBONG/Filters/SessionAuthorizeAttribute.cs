using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace QUANLYSANBONG.Filters;

public class SessionAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string[] _roles;

    public SessionAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

        if (hasAllowAnonymous)
        {
            base.OnActionExecuting(context);
            return;
        }

        var vaiTro = context.HttpContext.Session.GetString("VaiTro");

        if (string.IsNullOrWhiteSpace(vaiTro))
        {
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl = context.HttpContext.Request.Path.Value });
            return;
        }

        if (_roles.Length > 0 && !_roles.Contains(vaiTro))
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Content = "Ban khong co quyen truy cap chuc nang nay."
            };
            return;
        }

        base.OnActionExecuting(context);
    }
}
