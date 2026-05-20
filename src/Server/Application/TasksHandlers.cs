using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using global::Mediator;

namespace Server.Application.Tasks
{
    internal sealed class ListTasksHandler : global::Mediator.IRequestHandler<ListTasksQuery, Server.Presentation.Controllers.TaskList>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public ListTasksHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.TaskList> Handle(ListTasksQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetTasksAsync(request.Q, request.Tag, request.Completed, request.Sort, request.Page, request.PageSize);
        }
    }

    internal sealed class CreateTaskHandler : global::Mediator.IRequestHandler<CreateTaskCommand, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public CreateTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _repo.CreateAsync(request.Body);
        }
    }

    internal sealed class GetTaskHandler : global::Mediator.IRequestHandler<GetTaskQuery, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public GetTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var t = await _repo.GetAsync(request.Id);
            if (t == null) throw new KeyNotFoundException("Task not found");
            return t;
        }
    }

    internal sealed class ReplaceTaskHandler : global::Mediator.IRequestHandler<ReplaceTaskCommand, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public ReplaceTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(ReplaceTaskCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.ReplaceAsync(request.Id, request.Body);
            if (t == null) throw new KeyNotFoundException("Task not found");
            return t;
        }
    }

    internal sealed class PatchTaskHandler : global::Mediator.IRequestHandler<PatchTaskCommand, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public PatchTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(PatchTaskCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.PatchAsync(request.Id, request.Body);
            if (t == null) throw new KeyNotFoundException("Task not found");
            return t;
        }
    }

    internal sealed class DeleteTaskHandler : global::Mediator.IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public DeleteTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id);
        }
    }

    internal sealed class CompleteTaskHandler : global::Mediator.IRequestHandler<CompleteTaskCommand, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public CompleteTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.SetCompletedAsync(request.Id, true);
            if (t == null) throw new KeyNotFoundException("Task not found");
            return t;
        }
    }

    internal sealed class UncompleteTaskHandler : global::Mediator.IRequestHandler<UncompleteTaskCommand, Server.Presentation.Controllers.Task>
    {
        private readonly Server.Application.Interfaces.ITaskRepository _repo;
        public UncompleteTaskHandler(Server.Application.Interfaces.ITaskRepository repo) => _repo = repo;
        public async ValueTask<Server.Presentation.Controllers.Task> Handle(UncompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var t = await _repo.SetCompletedAsync(request.Id, false);
            if (t == null) throw new KeyNotFoundException("Task not found");
            return t;
        }
    }
}
