using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Marwalsch.Maui.Model;
using Marwalsch.Maui.Model.Types;
using Marwalsch.MauiApp.Model.Services;

namespace Marwalsch.Maui.Pages;

public partial class AssignmentOverviewPageViewModel(IRemoteAssignmentService assignmentService) : ViewModelBase
{
    private readonly IRemoteAssignmentService _assignmentService = assignmentService ?? throw new ArgumentNullException(nameof(assignmentService));

    public ObservableCollection<AssignmentBase> Assignments { get; } = [];

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    bool hideCompletedItems;

    [RelayCommand]
    async Task GetAssignmentsAsync()
    {
        try
        {
            IsBusy = true;

            var assignments = await _assignmentService.FetchAssigmentsAsync(100, 0);

            if (Assignments.Count != 0)
            {
                Assignments.Clear();
            }
            foreach (var assignment in assignments.Where(a => a.Status != Status.Completed || !HideCompletedItems))
            {
                Assignments.Add(assignment);
            }

        }
        catch (Exception ex)
        {
            await DisplayErrorAsync(ex);
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(AssignmentBase? assignment = null)
    {
        if (assignment == null)
        {
            IsBusy = true;

            try
            {
                assignment = new()
                {
                    Id = null,
                    Title = "Enter title",
                    Description = "Enter description",
                    Deadline = DateTimeOffset.Now.AddDays(3),
                    Status = Status.Pending
                };
                assignment.Id = await _assignmentService.CreateAssignmentAsync(assignment);
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
        }
        await Shell.Current.GoToAsync(nameof(AssignmentDetailsPage), true, new Dictionary<string, object>
        {
            { "Assignment", assignment! }
        });
    }
}