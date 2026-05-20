using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<Server.Presentation.Controllers.TaskList> GetTasksAsync(string q, string tag, bool? completed, string sort, int page, int pageSize);
        Task<Server.Presentation.Controllers.Task> GetAsync(Guid id);
        Task<Server.Presentation.Controllers.Task> CreateAsync(Server.Presentation.Controllers.TaskCreate create);
        Task<Server.Presentation.Controllers.Task> ReplaceAsync(Guid id, Server.Presentation.Controllers.TaskCreate create);
        Task<Server.Presentation.Controllers.Task> PatchAsync(Guid id, Server.Presentation.Controllers.TaskUpdate update);
        Task<bool> DeleteAsync(Guid id);
        Task<Server.Presentation.Controllers.Task> SetCompletedAsync(Guid id, bool completed);
    }
}
