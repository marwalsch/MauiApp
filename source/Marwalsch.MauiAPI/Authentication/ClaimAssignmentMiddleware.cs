using System.Security.Claims;

namespace Marwalsch.MauiAPI.Authentication;

public class ClaimAssignmentMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var upn = context.User.Claims
                .ToList()
                .FirstOrDefault(c => c.Type == ClaimTypes.Upn);

            if (upn != null)
            {
                context.User.Identities
                    .First()
                    .AddClaim(new Claim(Claims.AuthorId, upn.Value));
            }
        }
        await _next(context);
    }
}
