using Marwalsch.Maui.Model.Types;

namespace Marwalsch.Maui.Model;

public class AssignmentBase
{
    public Guid? Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset? Deadline { get; set; }

    public Status Status { get; set; } = Status.Pending;
}
