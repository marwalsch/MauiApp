using Marwalsch.Maui.Model;

namespace Marwalsch.MauiApp.Model.Services;

public interface IRemoteAssignmentService
{
    public Task<IEnumerable<AssignmentBase>> FetchAssigmentsAsync(int take, int skip = 0, CancellationToken? token = default);

    public Task<bool> DeleteAssignmentAsync(Guid id, CancellationToken? token = default);

    public Task<bool> UpdateAssignmentAsync(AssignmentBase assignment, CancellationToken? token = default);

    public Task<Guid> CreateAssignmentAsync(AssignmentBase assignment, CancellationToken? token = default);
}
