using Host = Microsoft.Maui.Hosting;

namespace Marwalsch.Maui.WinUI;

public static class MauiProgram
{
    public static Host.MauiApp CreateMauiApp()
    {
        var builder = Host.MauiApp.CreateBuilder();

        builder
            .UseSharedMauiApp();

        return builder.Build();
    }
}
