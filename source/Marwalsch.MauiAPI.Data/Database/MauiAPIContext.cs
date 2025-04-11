using Marwalsch.Maui.Model;
using Marwalsch.Maui.Model.Types;
using Marwalsch.MauiAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Marwalsch.MauiAPI.Data.Database;

public class MauiAPIContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseInMemoryDatabase(nameof(MauiAPIContext));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assignmentEntity = modelBuilder.Entity<Assignment>();

        assignmentEntity
            .HasKey(a => a.Id);

        List<Assignment> assignments = [];

        for (var i = 1; i <= 16; i++)
        {
            assignments.Add(Create(Guid.NewGuid(), i));
        }

        assignmentEntity.HasData(assignments);

        Assignment Create(Guid id, int index) => new()
        {
            Id = id,
            Title = $"Assignment {index}",
            Description = $"Description for Assignment {index}",
            Deadline = DateTimeOffset.Now.AddDays(index * 10),
            Status = index % 2 == 0 ? Status.Pending : Status.Completed,
            AuthorId = index % 3 == 0 ? Common.AnotherAuthorId : Common.AuthorId
        };
    }
}
