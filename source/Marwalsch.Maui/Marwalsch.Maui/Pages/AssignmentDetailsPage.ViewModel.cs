using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Marwalsch.Maui.Model;
using Marwalsch.Maui.Model.Types;
using Marwalsch.MauiApp.Model.Services;

namespace Marwalsch.Maui.Pages;

[QueryProperty(nameof(PassedAssignment), "Assignment")]
public partial class AssignmentDetailsPageViewModel(IRemoteAssignmentService assignmentService) : ViewModelBase
{
    private readonly IRemoteAssignmentService _assignmentService = assignmentService ?? throw new ArgumentNullException(nameof(assignmentService));

    private AssignmentBase? _assignment;

    public AssignmentBase PassedAssignment
    {
        get => _assignment!;
        set
        {
            _assignment = value;

            if (value != null)
            {
                Title = value.Title;
                Description = value.Description;
                Deadline = value.Deadline;
                IsCompleted = value.Status == Model.Types.Status.Completed;
            }
        }
    }

    [ObservableProperty]
    string? title;

    [ObservableProperty]
    string? description;

    [ObservableProperty]
    DateTimeOffset? deadline;

    [ObservableProperty]
    bool isCompleted;

    [RelayCommand]
    async Task SaveAsync()
    {
        IsBusy = true;

        try
        {
            var ass = new AssignmentBase()
            {
                Id = PassedAssignment.Id,
                Deadline = Deadline,
                Description = Description,
                Title = Title,
                Status = IsCompleted ? Status.Completed : Status.Pending
            };

            await _assignmentService.UpdateAssignmentAsync(new() 
            { 
                Id = PassedAssignment.Id,
                Deadline = Deadline,
                Description = Description,
                Title = Title,
                Status = IsCompleted ? Status.Completed : Status.Pending
            });
        }
        catch (Exception ex)
        {
            await DisplayErrorAsync(ex);
        }
        finally
        {
            IsBusy = false;
        }
        await Shell.Current.DisplayAlert("Success", "Assignment saved successfully.", "OK");
    }

    [RelayCommand]
    async Task DeleteAsync()
    {
        IsBusy = true;

        //var assi = (Guid)PassedAssignment.Id!;

        var guid = (Guid)PassedAssignment.Id!;

        try
        {
            var y = await _assignmentService.DeleteAssignmentAsync(guid);
        }
        catch (Exception ex)
        {
            await DisplayErrorAsync(ex);

            return;
        }
        finally
        {
            IsBusy = false;
        }
        //await Shell.Current.GoToAsync($"///{nameof(AssignmentOverviewPage)}", false/*, new Dictionary<string, object> { }*/);

        await Shell.Current.DisplayAlert("Success", "Assignment deleted successfully.", "OK");
    }
}
