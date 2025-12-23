using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControl.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
	public void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.ToTable("Transactions");

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).ValueGeneratedNever();

		builder.Property(t => t.Description)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(t => t.Amount)
			.IsRequired()
			.HasPrecision(18, 2);

		builder.Property(t => t.Date)
			.IsRequired()
			.HasColumnType("date");

		builder.HasIndex(t => t.Date);

		builder.Property(t => t.Type)
			.IsRequired()
			.HasConversion<string>()
			.HasMaxLength(20);

		builder.Property(t => t.CreatedAt)
			.IsRequired();

		builder.HasOne(t => t.Category)
			.WithMany()
			.HasForeignKey(t => t.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

