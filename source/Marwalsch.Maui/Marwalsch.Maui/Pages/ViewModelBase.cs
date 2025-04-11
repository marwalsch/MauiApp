using CommunityToolkit.Mvvm.ComponentModel;

namespace Marwalsch.Maui.Pages;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    protected async Task DisplayErrorAsync(Exception ex)
    {
        System.Diagnostics.Debug.WriteLine(ex);

        await Shell.Current.DisplayAlert("An error occured.", ex.Message, "Dismiss");
    }
}
