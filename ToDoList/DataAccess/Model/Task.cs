using ToDoList.DataAccess.Enums;

namespace ToDoList.DataAccess.Model
{
    public class Task
    {
        public Task()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
		public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
		public TaskState TaskState { get; set; }
        public DateOnly EndDate {  get; set; }
		public TimeOnly EndTime { get; set; }
		public User? User { get; set; }
	}
}
