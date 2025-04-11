namespace Marwalsch.Maui.Pages;

public partial class AssignmentDetailsPage : ContentPage
{
	public AssignmentDetailsPage(AssignmentDetailsPageViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}