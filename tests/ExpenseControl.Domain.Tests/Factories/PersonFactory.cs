using Bogus;

namespace ExpenseControl.Domain.Tests.Factories;

public static class PersonFactory
{
	public static Domain.Entities.Person Create()
	{
		return new Faker<Domain.Entities.Person>("pt_BR")
			.CustomInstantiator(f => new Domain.Entities.Person(
				f.Name.FullName(),
				f.Date.Past(72, DateTime.UtcNow.AddYears(-18)) 
			))
			.Generate();
	}

	public static Domain.Entities.Person CreateMinor()
	{
		return new Faker<Domain.Entities.Person>("pt_BR")
			.CustomInstantiator(f => new Domain.Entities.Person(
				f.Name.FullName(),
				f.Date.Past(17, DateTime.UtcNow)
			))
			.Generate();
	}
}
