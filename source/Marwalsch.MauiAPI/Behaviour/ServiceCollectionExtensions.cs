using Marwalsch.Maui.Model;
using Microsoft.OpenApi.Models;

namespace Marwalsch.MauiAPI.Behaviour;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMauiAPISwaggerDashboard(this IServiceCollection services) => services
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc(Common.ApiVersion, new OpenApiInfo { Title = nameof(MauiAPI), Version = Common.ApiVersion });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = "bearer",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Description = $"Type '{Common.AuthorId}'"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new ()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
}
