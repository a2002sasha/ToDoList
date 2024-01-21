using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.DataAccess.EntityConfigurations;
using ToDoList.DataAccess.Model;

namespace ToDoList.DataAccess.Context
{
    public class ToDoContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<Model.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new TaskConfiguration());
			base.OnModelCreating(builder);
        }
    }
}
