using Marwalsch.Maui.Pages;

namespace Marwalsch.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AssignmentDetailsPage), typeof(AssignmentDetailsPage));
        Routing.RegisterRoute(nameof(AssignmentOverviewPage), typeof(AssignmentOverviewPage));
    }
}
