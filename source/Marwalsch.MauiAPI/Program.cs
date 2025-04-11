using Marwalsch.MauiAPI.Data;
using Marwalsch.MauiAPI.Behaviour;
using Marwalsch.MauiAPI.Authentication;

namespace Marwalsch.MauiAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureServices();

        var application = builder.Build();

        application
            .UseMauiAPIExceptionBoundary()
            .UseHttpsRedirection()
            .UseMauiAPISwaggerDashboard()
            .UseMauiAPIAuthentication()
            .UseAuthorization();

        application.MapControllers();
        application.Run();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services
            .AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

        services
            .AddEndpointsApiExplorer()
            .AddMauiAPIData()
            .AddMauiAPIAuthentication()
            .AddMauiAPISwaggerDashboard();
    }
}
