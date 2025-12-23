using Bogus;
using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Tests.Factories;

public static class RefreshTokenFactory
{
	public static RefreshToken Create(Guid? userId = null, string? tokenHash = null, DateTime? expires = null)
	{
		return new Faker<RefreshToken>("pt_BR")
			.CustomInstantiator(f => new RefreshToken(
				userId ?? Guid.NewGuid(),
				tokenHash ?? f.Random.AlphaNumeric(32),
				expires ?? DateTime.UtcNow.AddDays(7) 
			))
			.Generate();
	}
}
