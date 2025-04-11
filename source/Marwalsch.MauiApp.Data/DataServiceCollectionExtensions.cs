using System.Net.Http.Headers;
using Marwalsch.MauiApp.Model.Services;
using Marwalsch.MauiApp.Data.Remote;
using Microsoft.Extensions.DependencyInjection;

namespace Marwalsch.MauiApp.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddDMauiAppDataServices(this IServiceCollection services, Action<MauiAppDataOptions> options)
    {
        var mauiAppDataOptions = new MauiAppDataOptions();

        options.Invoke(mauiAppDataOptions);

        services.AddSingleton<IRemoteAssignmentService, HttpAssignmentService>();
        services.AddHttpClient<IRemoteAssignmentService, HttpAssignmentService>((provider, client) =>
        {
            var url = $"{mauiAppDataOptions.BaseUrl ?? throw new ArgumentException(nameof(MauiAppDataOptions.BaseUrl))}/Assignments";

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mauiAppDataOptions.ApiKey);
        });
        services.Configure(options);

        return services;
    }
}
