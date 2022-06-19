using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class TasksContext : DbContext
    {
        private readonly string _connectionString;

        public TasksContext(string connectionString)
        {
            _connectionString = connectionString;
        }

      public DbSet<TaskItem> TaskItems{ get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

}
