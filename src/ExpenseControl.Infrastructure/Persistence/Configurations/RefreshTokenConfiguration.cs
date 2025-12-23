using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseControl.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.ToTable("RefreshTokens");

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).ValueGeneratedNever();

		builder.Property(t => t.TokenHash)
			.IsRequired()
			.HasMaxLength(500);

		builder.HasIndex(t => t.TokenHash);

		builder.Property(t => t.Expires)
			.IsRequired();

		builder.Property(t => t.IsRevoked)
			.IsRequired();

		builder.Property(t => t.CreatedAt)
			.IsRequired();

		builder.Ignore(t => t.IsActive);

		builder.HasOne(t => t.User)
			.WithMany()
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

