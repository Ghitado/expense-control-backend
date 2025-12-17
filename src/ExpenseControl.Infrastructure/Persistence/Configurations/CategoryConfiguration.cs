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

		builder.Property(p => p.Id)
			.ValueGeneratedNever();

		builder.Property(c => c.Description)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(c => c.Purpose)
			.IsRequired();
	}
}

