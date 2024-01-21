using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DataAccess.EntityConfigurations
{
	public class TaskConfiguration : IEntityTypeConfiguration<Model.Task>
	{
		public void Configure(EntityTypeBuilder<Model.Task> builder)
		{
			builder.ToTable("Tasks")
				   .HasKey(t => t.Id);

			builder.HasOne(p => p.User)
				   .WithMany(u => u.Tasks)
				   .HasForeignKey(p => p.UserId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.Property(t => t.PriorityLevel)
				   .IsRequired();

			builder.Property(t => t.TaskState)
				   .IsRequired();

			builder.Property(t => t.Name)
				   .HasMaxLength(50)
				   .IsRequired();

			builder.Property(t => t.Description)
				   .HasMaxLength(1000)
				   .IsRequired(false);

			builder.Property(t => t.EndDate)
				   .IsRequired();

			builder.Property(t => t.EndTime)
				   .IsRequired();

		}
	}
}
