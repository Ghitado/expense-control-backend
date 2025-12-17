using Bogus;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Domain.Tests.Factories;

public static class CategoryFactory
{
	public static Category Create(CategoryPurpose? purpose = null)
	{
		return new Faker<Category>("pt_BR")
			.CustomInstantiator(f => new Category(
				f.Commerce.Department(), 
				purpose ?? f.PickRandom<CategoryPurpose>()
			))
			.Generate();
	}
}

