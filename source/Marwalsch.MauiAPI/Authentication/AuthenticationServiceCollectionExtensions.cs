using Microsoft.AspNetCore.Authentication;

namespace Marwalsch.MauiAPI.Authentication;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddMauiAPIAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(NaiveAuthenticationHandler.SchemeName)   
            .AddScheme<AuthenticationSchemeOptions, NaiveAuthenticationHandler>(NaiveAuthenticationHandler.SchemeName, null);

        return services;
    }
}
