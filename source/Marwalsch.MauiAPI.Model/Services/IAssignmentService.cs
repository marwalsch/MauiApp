using System.Linq.Expressions;

namespace Marwalsch.MauiAPI.Model.Services;

public interface IAssignmentService
{
    IAsyncEnumerable<Assignment> GetAsync(int take, int skip, Expression<Func<Assignment, bool>> filter, Expression<Func<Assignment, object>> orderBy, CancellationToken cancellationToken);

    Task UpdateAsync(Assignment assignment, CancellationToken token = default);

    Task AddAsync(Assignment assignment, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}
