using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Infrastructure
{
    using TaskDto = Server.Presentation.Controllers.Task;
    using TaskCreateDto = Server.Presentation.Controllers.TaskCreate;
    using TaskUpdateDto = Server.Presentation.Controllers.TaskUpdate;
    using TaskListDto = Server.Presentation.Controllers.TaskList;

    public class InMemoryTaskRepository : Server.Application.Interfaces.ITaskRepository
    {
        private readonly ConcurrentDictionary<Guid, TaskDto> _store = new();

        public InMemoryTaskRepository()
        {
            var sample = new TaskDto
            {
                Id = Guid.NewGuid(),
                Title = "Sample task",
                Content = "This is a seeded task.",
                CreatedAt = DateTimeOffset.UtcNow,
                Completed = false,
                Tags = new System.Collections.Generic.List<string>{ "sample" }
            };
            _store[sample.Id] = sample;
        }

        public Task<TaskListDto> GetTasksAsync(string? q, string? tag, bool? completed, string? sort, int page, int pageSize)
        {
            IEnumerable<TaskDto> items = _store.Values;

            if (!string.IsNullOrWhiteSpace(q))
            {
                var lower = q.ToLowerInvariant();
                items = items.Where(x => (x.Title ?? "").ToLowerInvariant().Contains(lower) || (x.Content ?? "").ToLowerInvariant().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(tag))
            {
                var wanted = tag.Split(',').Select(t => t.Trim()).Where(t => t.Length>0).ToArray();
                if (wanted.Length > 0)
                    items = items.Where(x => x.Tags != null && x.Tags.Any(t => wanted.Contains(t)));
            }
            if (completed.HasValue)
                items = items.Where(x => x.Completed == completed.Value);

            bool desc = false;
            var field = sort ?? "";
            if (field.StartsWith("-"))
            {
                desc = true;
                field = field.Substring(1);
            }
            IOrderedEnumerable<TaskDto> ordered;
            if (string.Equals(field, "dueDate", StringComparison.OrdinalIgnoreCase))
            {
                ordered = desc ? items.OrderByDescending(x => x.DueDate) : items.OrderBy(x => x.DueDate);
            }
            else
            {
                ordered = desc ? items.OrderByDescending(x => x.CreatedAt) : items.OrderBy(x => x.CreatedAt);
            }

            var total = ordered.Count();
            var pageIndex = Math.Max(1, page);
            var skip = (pageIndex - 1) * pageSize;
            var pageItems = ordered.Skip(skip).Take(pageSize).ToList();

            var list = new TaskListDto
            {
                Items = pageItems,
                Total = total,
                Page = pageIndex,
                PageSize = pageSize
            };
            return Task.FromResult(list);
        }

        public Task<TaskDto?> GetAsync(Guid id){ _store.TryGetValue(id, out var t); return Task.FromResult<TaskDto?>(t); }

        public Task<TaskDto> CreateAsync(TaskCreateDto create)
        {
            var dto = new TaskDto
            {
                Id = Guid.NewGuid(),
                Title = create.Title,
                Content = create.Content,
                DueDate = create.DueDate == default ? null : create.DueDate,
                Completed = false,
                Tags = create.Tags ?? new List<string>(),
                CreatedAt = DateTimeOffset.UtcNow
            };
            _store[dto.Id] = dto;
            return Task.FromResult(dto);
        }

        public Task<TaskDto?> ReplaceAsync(Guid id, TaskCreateDto create)
        {
            if (!_store.TryGetValue(id, out var existing)) return Task.FromResult<TaskDto?>(null);
            var dto = new TaskDto
            {
                Id = id,
                Title = create.Title,
                Content = create.Content,
                DueDate = create.DueDate == default ? null : create.DueDate,
                Completed = false,
                Tags = create.Tags ?? new List<string>(),
                CreatedAt = existing.CreatedAt,
                UpdatedAt = DateTimeOffset.UtcNow
            };
            _store[id] = dto;
            return Task.FromResult<TaskDto?>(dto);
        }

        public Task<TaskDto?> PatchAsync(Guid id, TaskUpdateDto update)
        {
            if (!_store.TryGetValue(id, out var existing)) return Task.FromResult<TaskDto?>(null);
            if (update.Title != null) existing.Title = update.Title;
            if (update.Content != null) existing.Content = update.Content;
            if (update.DueDate != default(DateTimeOffset)) existing.DueDate = update.DueDate;
            existing.Completed = update.Completed;
            if (update.Tags != null) existing.Tags = update.Tags;
            existing.UpdatedAt = DateTimeOffset.UtcNow;
            _store[id] = existing;
            return Task.FromResult<TaskDto?>(existing);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return Task.FromResult(_store.TryRemove(id, out _));
        }

        public Task<TaskDto?> SetCompletedAsync(Guid id, bool completed)
        {
            if (!_store.TryGetValue(id, out var existing)) return Task.FromResult<TaskDto?>(null);
            existing.Completed = completed;
            existing.UpdatedAt = DateTimeOffset.UtcNow;
            _store[id] = existing;
            return Task.FromResult<TaskDto?>(existing);
        }
    }
}
