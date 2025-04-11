using Marwalsch.MauiAPI.Data.Database;
using Marwalsch.MauiAPI.Data.Services;
using Marwalsch.MauiAPI.Model.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Marwalsch.MauiAPI.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddMauiAPIData(this IServiceCollection services) => services
        .AddTransient<IAssignmentService, DbAssignmentService>()
        .AddDbContextFactory<MauiAPIContext>();
}