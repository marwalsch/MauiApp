using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace Marwalsch.MauiAPI.Authentication;

public class NaiveAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "Naive";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeader) && !authorizationHeader.ToString().StartsWith("Bearer "))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authentication scheme."));
        }
        var bearerToken = authorizationHeader
            .ToString()["Bearer ".Length..]
            .Trim();

        if (!Guid.TryParse(bearerToken, out var authorId))
        {
            return Task.FromResult(AuthenticateResult.Fail("Malformed bearer token."));
        }
        var claims = new[] { new Claim(ClaimTypes.Upn, authorId.ToString()) };
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}