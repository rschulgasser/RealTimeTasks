using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Data
{
    public class TasksRepository
    {
        private readonly string _connectionString;

        public TasksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<TaskItem> GetTasks()
        {
            using var ctx = new TasksContext(_connectionString);
            return ctx.TaskItems.Include(t => t.User).ToList();
        }
        public void AddTask(TaskItem task)
        {
           
            using var ctx = new TasksContext(_connectionString);
            ctx.TaskItems.Add(task);
            ctx.SaveChanges();
        }
        public void MarkAsDoingTask(TaskItem task)
        {

            using var ctx = new TasksContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"Update TaskItems SET Status={Status.InProgressByOtherUser}, UserId={task.UserId} WHERE Id = {task.Id}");
            ctx.SaveChanges();
        }
        public void MarkAsDoneTask(int id)
        {

            using var ctx = new TasksContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM TaskItems WHERE Id = {id}");
            ctx.SaveChanges();
        }


    }
  
}
