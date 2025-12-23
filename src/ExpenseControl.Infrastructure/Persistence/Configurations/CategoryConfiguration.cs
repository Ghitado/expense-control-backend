using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControl.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories");

		builder.HasKey(c => c.Id);
		builder.Property(c => c.Id).ValueGeneratedNever();

		builder.Property(c => c.Name)
			.IsRequired()
			.HasMaxLength(150);

		builder.HasIndex(c => c.Name)
			.IsUnique();

		builder.Property(c => c.Purpose)
			.IsRequired()
			.HasConversion<string>()
			.HasMaxLength(20);

		builder.Property(c => c.CreatedAt)
			.IsRequired();
	}
}
