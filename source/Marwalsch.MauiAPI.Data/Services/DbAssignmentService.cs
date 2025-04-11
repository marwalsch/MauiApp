using System.Linq.Expressions;
using System.Runtime.CompilerServices;
//using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Marwalsch.MauiAPI.Model;
using Marwalsch.MauiAPI.Model.Services;
using Marwalsch.MauiAPI.Data.Database;

namespace Marwalsch.MauiAPI.Data.Services;

public class DbAssignmentService(IDbContextFactory<MauiAPIContext> contextFactory/*, ILogger<DbAssignmentService> logger*/) : IAssignmentService
{
    private readonly IDbContextFactory<MauiAPIContext> _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));

    //private readonly ILogger<DbAssignmentService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async IAsyncEnumerable<Assignment> GetAsync(int take, int skip, Expression<Func<Assignment, bool>> filter, Expression<Func<Assignment, object>> orderBy, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var context = await GetContextAsync();

        var query = context.Assignments
           .Where(filter)
           .OrderBy(orderBy)
           .Skip(skip)
           .Take(take)
           .AsAsyncEnumerable();

        await foreach (var type in query)
        {
            cancellationToken.ThrowIfCancellationRequested();

            yield return type;
        }
    }

    public async Task UpdateAsync(Assignment assignment, CancellationToken token = default)
    {
        var context = await GetContextAsync();

        context.Assignments.Update(assignment);

        await context.SaveChangesAsync(token);
    }

    public async Task AddAsync(Assignment assignment, CancellationToken token = default)
    {
        var context = await GetContextAsync();

        context.Assignments.Add(assignment);

        await context.SaveChangesAsync(token);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
    {
        var context = await GetContextAsync();

        var entity = await context.Assignments.FindAsync([id], token);

        if (entity == null)
        {
            return false;
        }
        context.Assignments.Remove(entity);

        await context.SaveChangesAsync(token);

        return true;
    }

    protected async Task<MauiAPIContext> GetContextAsync()
    {
        var context = _contextFactory.CreateDbContext();

        await context.Database.EnsureCreatedAsync();

        return context;
    }
}
