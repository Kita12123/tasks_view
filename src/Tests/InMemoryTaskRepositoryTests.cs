using System;
using Xunit;
using Server.Infrastructure;
using Server.Presentation.Controllers;

namespace Tests
{
    public class InMemoryTaskRepositoryTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetTasks_ReturnsSeededItem()
        {
            var repo = new InMemoryTaskRepository();
            var list = await repo.GetTasksAsync(null, null, null, null, 1, 25);
            Assert.NotNull(list);
            Assert.NotNull(list.Items);
            Assert.True(list.Items.Count >= 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_And_Get_Task()
        {
            var repo = new InMemoryTaskRepository();
            var create = new TaskCreate
            {
                Title = "Test Task",
                Content = "Content",
                DueDate = default(System.DateTimeOffset),
                Tags = new System.Collections.Generic.List<string>{ "t1" }
            };
            var created = await repo.CreateAsync(create);
            Assert.NotNull(created);
            Assert.Equal("Test Task", created.Title);

            var fetched = await repo.GetAsync(created.Id);
            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched.Id);
            Assert.Equal("Test Task", fetched.Title);
        }
    }
}
