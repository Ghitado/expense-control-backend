using Bogus;
using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Tests.Factories;

public static class UserFactory
{
	public static User Create(string? email = null, string? passwordHash = null)
	{
		return new Faker<User>("pt_BR")
			.CustomInstantiator(f => new User(
				email ?? f.Internet.Email().ToLower(),
				passwordHash ?? f.Random.AlphaNumeric(60)
			))
			.Generate();
	}
}
