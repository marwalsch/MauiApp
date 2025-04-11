using System.Net;
using System.Net.Http.Json;
using Marwalsch.Maui.Model;
using Marwalsch.MauiApp.Model.Services;

namespace Marwalsch.MauiApp.Data.Remote;

public class HttpAssignmentService(HttpClient client) : IRemoteAssignmentService
{
    private readonly HttpClient _httpClient = client ?? throw new ArgumentNullException(nameof(client));

    public async Task<Guid> CreateAssignmentAsync(AssignmentBase assignment, CancellationToken? token)
    {
        var response = await _httpClient.PostAsJsonAsync($"", assignment, token ?? CancellationToken.None);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AssignmentBase>(cancellationToken: token ?? CancellationToken.None);

        return result!.Id ?? throw new NullReferenceException(nameof(result.Id));
    }

    public async Task<bool>DeleteAssignmentAsync(Guid id, CancellationToken? token)
    {
        var response = await _httpClient.DeleteAsync($"Assignments/{id}", token ?? CancellationToken.None);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();

        return true;
    }

    public async Task<IEnumerable<AssignmentBase>> FetchAssigmentsAsync(int take, int skip, CancellationToken? token)
    {
        var response = await _httpClient.GetAsync($"?take={take}&skip={skip}", token ?? CancellationToken.None);

        response.EnsureSuccessStatusCode();

        var assignments = await response.Content.ReadFromJsonAsync<IEnumerable<AssignmentBase>>(cancellationToken: token ?? CancellationToken.None);

        return assignments!;
    }

    public async Task<bool> UpdateAssignmentAsync(AssignmentBase assignment, CancellationToken? token)
    {
        var response = await _httpClient.PutAsJsonAsync($"Assignments/{assignment.Id}", assignment, token ?? CancellationToken.None);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();

        return true;
    }
}
