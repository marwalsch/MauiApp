using Host = Microsoft.Maui.Hosting;

namespace Marwalsch.Maui.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override Host.MauiApp CreateMauiApp()
        => MauiProgram.CreateMauiApp();
}
