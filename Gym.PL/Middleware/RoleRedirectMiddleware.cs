using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class RoleRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public RoleRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;

        if (user.Identity.IsAuthenticated)
        {
            var path = context.Request.Path.Value.ToLower();

            // Allow logout to pass
            if (path.StartsWith("/account/logout"))
            {
                await _next(context);
                return;
            }

            // ======= ADMIN REDIRECT =======
            if (user.IsInRole("Admin"))
            {
                if (!path.StartsWith("/admin"))
                {
                    context.Response.Redirect("/AdminHome/Index");
                    return;
                }
            }

            // ======= TRAINER REDIRECT =======
            if (user.IsInRole("Trainer"))
            {
                if (!path.StartsWith("/trainer"))
                {
                    context.Response.Redirect("/TrainerHome/Index");
                    return;
                }
            }
        }

        await _next(context);
    }
}
