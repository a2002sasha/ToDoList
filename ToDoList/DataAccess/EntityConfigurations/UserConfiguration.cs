using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DataAccess.Model;

namespace ToDoList.DataAccess.EntityConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.FirstName)
				   .HasMaxLength(30)
				   .IsRequired();

			builder.Property(u => u.LastName)
				   .HasMaxLength(30)
				   .IsRequired();
		}
	}

}