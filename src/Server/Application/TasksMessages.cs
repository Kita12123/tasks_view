using System;
using global::Mediator;

namespace Server.Application.Tasks
{
    public sealed record ListTasksQuery(string? Q, string? Tag, bool? Completed, string? Sort, int Page, int PageSize) : global::Mediator.IRequest<Server.Presentation.Controllers.TaskList>;
    public sealed record CreateTaskCommand(Server.Presentation.Controllers.TaskCreate Body) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
    public sealed record GetTaskQuery(Guid Id) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
    public sealed record ReplaceTaskCommand(Guid Id, Server.Presentation.Controllers.TaskCreate Body) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
    public sealed record PatchTaskCommand(Guid Id, Server.Presentation.Controllers.TaskUpdate Body) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
    public sealed record DeleteTaskCommand(Guid Id) : global::Mediator.IRequest<bool>;
    public sealed record CompleteTaskCommand(Guid Id) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
    public sealed record UncompleteTaskCommand(Guid Id) : global::Mediator.IRequest<Server.Presentation.Controllers.Task>;
}
