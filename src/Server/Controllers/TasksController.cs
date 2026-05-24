using System;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Tasks;
using global::Mediator;

namespace Server.Presentation.Controllers;

[ApiController]
[Route("api")]
public class TasksController : ControllerBase
{
    private readonly global::Mediator.IMediator _mediator;

    public TasksController(global::Mediator.IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public override async System.Threading.Tasks.Task<TaskList> ListTasks(string? q = null, string? tag = null, bool? completed = null, string? sort = null, int? page = 1, int? pageSize = 25)
    {
        var req = new ListTasksQuery(q, tag, completed, sort, page ?? 1, pageSize ?? 25);
        return await _mediator.Send(req);
    }

    public override async System.Threading.Tasks.Task<Task> CreateTask(TaskCreate body)
    {
        var req = new CreateTaskCommand(body);
        return await _mediator.Send(req);
    }

    public override async System.Threading.Tasks.Task<Task> GetTask(string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        var req = new GetTaskQuery(id);
        return await _mediator.Send(req);
    }

    public override async System.Threading.Tasks.Task<Task> ReplaceTask(TaskCreate body, string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        var req = new ReplaceTaskCommand(id, body);
        return await _mediator.Send(req);
    }

    public override async System.Threading.Tasks.Task<Task> UpdateTask(TaskUpdate body, string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        var req = new PatchTaskCommand(id, body);
        return await _mediator.Send(req);
    }

    public override async System.Threading.Tasks.Task DeleteTask(string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        var ok = await _mediator.Send(new DeleteTaskCommand(id));
        if (!ok) throw new KeyNotFoundException("Task not found");
    }

    public override async System.Threading.Tasks.Task<Task> CompleteTask(string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        return await _mediator.Send(new CompleteTaskCommand(id));
    }

    public override async System.Threading.Tasks.Task<Task> UncompleteTask(string taskId)
    {
        if (!Guid.TryParse(taskId, out var id)) throw new ArgumentException("Invalid taskId", nameof(taskId));
        return await _mediator.Send(new UncompleteTaskCommand(id));
    }
}

