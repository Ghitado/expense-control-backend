using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControl.Infrastructure.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.ToTable("People");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedNever();

		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(p => p.Age)
			.IsRequired();

		builder.HasMany(p => p.Transactions)
			.WithOne(t => t.Person)
			.HasForeignKey(t => t.PersonId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Navigation(p => p.Transactions)
			.UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}

