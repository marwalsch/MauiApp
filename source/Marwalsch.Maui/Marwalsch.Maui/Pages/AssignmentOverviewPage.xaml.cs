namespace Marwalsch.Maui.Pages;

public partial class AssignmentOverviewPage : ContentPage
{
	public AssignmentOverviewPage(AssignmentOverviewPageViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}