using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;

namespace ToDoList.Web
{
    public class TasksHub : Hub
    {

        private string _connectionString;

        public TasksHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        public void GetTasks()
        {
            var repo = new TasksRepository(_connectionString);
            var tasks = repo.GetTasks();
            Clients.All.SendAsync("getTasks", tasks);
        }
        [Authorize]
        public void AddTask(TaskItem task)
        {
            task.Status = 0;
            var repo = new TasksRepository(_connectionString);
            repo.AddTask(task);
            Clients.All.SendAsync("addTask", repo.GetTasks());
        }
        [Authorize]
        public void MarkAsDoingTask(TaskItem task)
        {  
            
            task.UserId = GetCurrentUser().Id;
            var repo = new TasksRepository(_connectionString);
            repo.MarkAsDoingTask(task);
            var tasks = repo.GetTasks();
            Clients.All.SendAsync("markAsDoingTask", tasks);
        }
        [Authorize]
        public void MarkAsDoneTask(TaskItem task)
        {
            if (GetCurrentUser().Id != task.UserId)
            {
                return;
            }
            task.UserId = GetCurrentUser().Id;
            var repo = new TasksRepository(_connectionString);
            repo.MarkAsDoneTask(task.Id);
            var tasks = repo.GetTasks();
            Clients.All.SendAsync("markAsDoneTask", tasks);
        }

        private User GetCurrentUser()
        {
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(Context.User.Identity.Name);
            return user;
        }

    }
}
