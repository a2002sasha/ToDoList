using Microsoft.AspNetCore.Identity;

namespace ToDoList.DataAccess.Model
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
		public List<Task> Tasks { get; set; } = new List<Task>();
	}
}
