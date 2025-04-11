using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Marwalsch.Maui.Model;
using Marwalsch.Maui.Pages;
using Marwalsch.MauiApp.Data;

namespace Marwalsch.Maui;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.ConfigureServices();
        builder.Services.ConfigurePages();

        return builder;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services) 
    {
        services.AddDMauiAppDataServices(options => 
        {
            options.ApiKey = $"{Common.AuthorId}";
            options.BaseUrl = $"https://localhost:7135/api/{Common.ApiVersion}";
            // options.BaseUrl = $"https://{**********}.ngrok-free.app/api/{Common.ApiVersion}";
        });
        return services;
    }

    private static IServiceCollection ConfigurePages(this IServiceCollection services)
    {
        services.AddSingleton<AssignmentOverviewPage>();
        services.AddSingleton<AssignmentOverviewPageViewModel>();

        services.AddSingleton<AssignmentDetailsPage>();
        services.AddSingleton<AssignmentDetailsPageViewModel>();

        return services;
    }
}
