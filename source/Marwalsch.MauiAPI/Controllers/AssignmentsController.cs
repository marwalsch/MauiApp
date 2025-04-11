using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Marwalsch.Maui.Model;
using Marwalsch.MauiAPI.Model;
using Marwalsch.MauiAPI.Model.Services;
using Marwalsch.MauiAPI.Authentication;

namespace Marwalsch.MauiAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public sealed class AssignmentsController(/*ILogger<AssignmentsController> logger,*/ IAssignmentService assignments) : ControllerBase
{
    // private readonly ILogger<AssignmentsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IAssignmentService _assignments = assignments ?? throw new ArgumentNullException(nameof(assignments));

    [HttpGet]
    public IAsyncEnumerable<AssignmentBase> GetAsync(int take = 100, int skip = 0, CancellationToken token = default) 
        => _assignments.GetAsync(take, skip, a => a.AuthorId == GetAuthorId(), a => a.Deadline!, token);

    [HttpGet("{assignmentId}")]
    public async Task<ActionResult<AssignmentBase>> GetAsync(Guid assignmentId, CancellationToken token = default)
    {
        var assignment = await _assignments
            .GetAsync(1, 0, a => a.Id == assignmentId, a => a.Id, token)
            .FirstOrDefaultAsync(token);

        return assignment is not null ? Ok(assignment) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AssignmentBase assignment, CancellationToken token = default)
    {
        var newAssignment = new Assignment
        {
            Id = Guid.NewGuid(),
            AuthorId = GetAuthorId(),
            Title = assignment.Title,
            Description = assignment.Description,
            Deadline = assignment.Deadline,
            Status = assignment.Status
        };

        await _assignments.AddAsync(newAssignment, token);

        return CreatedAtAction(nameof(GetAsync), new { id = newAssignment.Id }, (AssignmentBase)newAssignment);
    }

    [HttpPut("{assignmentId}")]
    public async Task<IActionResult> PutAsync(Guid assignmentId, [FromBody] AssignmentBase assignment, CancellationToken token = default)
    {
        var updatedAssignment = new Assignment
        {
            Id = assignmentId,
            AuthorId = GetAuthorId(),
            Title = assignment.Title,
            Description = assignment.Description,
            Deadline = assignment.Deadline,
            Status = assignment.Status
        };

        await _assignments.UpdateAsync(updatedAssignment, token);

        return Ok(updatedAssignment);
    }

    [HttpDelete("{assignmentId}")]
    public async Task<IActionResult> DeleteAsync(Guid assignmentId, CancellationToken token = default) 
        => (await _assignments.DeleteAsync(assignmentId, token)) ? Ok() : NotFound();

    private Guid GetAuthorId() => Guid
        .Parse(HttpContext.User.Claims
            .First(c => c.Type == Claims.AuthorId).Value
            .ToString());
}
