using Android.App;
using Android.Runtime;
using Host = Microsoft.Maui.Hosting;

namespace Marwalsch.Maui.Droid;

[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership)
{
    protected override Host.MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
