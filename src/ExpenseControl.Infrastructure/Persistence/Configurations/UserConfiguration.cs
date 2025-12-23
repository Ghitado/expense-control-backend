using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControl.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(u => u.Id);
		builder.Property(u => u.Id).ValueGeneratedNever();

		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(255);

		builder.HasIndex(u => u.Email)
			.IsUnique();

		builder.Property(u => u.PasswordHash)
			.IsRequired()
			.HasMaxLength(500);

		builder.Property(u => u.CreatedAt)
			.IsRequired();
	}
}

