namespace ToDoList.Data
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
      
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}

