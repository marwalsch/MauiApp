namespace Marwalsch.MauiAPI.Authentication;

public static class AuthenticationApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMauiAPIAuthentication(this IApplicationBuilder app) => app
        .UseAuthentication()
        .UseMiddleware<ClaimAssignmentMiddleware>();
}
