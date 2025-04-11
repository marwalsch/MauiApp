using Marwalsch.Maui.Model;
using System.Net;

namespace Marwalsch.MauiAPI.Behaviour;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMauiAPISwaggerDashboard(this IApplicationBuilder app) => app
        .UseSwagger(c => c.RouteTemplate = "swagger/{documentname}/swagger.json")
        .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(MauiAPI)} {Common.ApiVersion}"));

    public static IApplicationBuilder UseMauiAPIExceptionBoundary(this IApplicationBuilder app) => app
        .UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync($"A Server Error occurred ({context.Connection.Id}).");

            });
        });
}
