using Bogus;

namespace ExpenseControl.Domain.Tests.Factories;

public static class PersonFactory
{
	public static Domain.Entities.Person Create()
	{
		return new Faker<Domain.Entities.Person>("pt_BR") 
			.CustomInstantiator(f => new Domain.Entities.Person(
				f.Name.FullName(),
				f.Random.Int(18, 90) 
			))
			.Generate();
	}

	public static Domain.Entities.Person CreateMinor()
	{
		return new Faker<Domain.Entities.Person>("pt_BR")
			.CustomInstantiator(f => new Domain.Entities.Person(
				f.Name.FullName(),
				f.Random.Int(1, 17) 
			))
			.Generate();
	}
}

