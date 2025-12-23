using Bogus;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Domain.Tests.Factories;

public static class TransactionFactory
{
	public static Transaction Create(TransactionType type, Category category, Guid? personId = null)
	{
		return new Faker<Transaction>("pt_BR")
			.CustomInstantiator(f => new Transaction(
				f.Commerce.ProductName(),
				decimal.Parse(f.Commerce.Price(10, 5000)), 
				f.Date.Recent(90), 
				type,
				category,
				personId ?? Guid.CreateVersion7()
			))
			.Generate();
	}
}
