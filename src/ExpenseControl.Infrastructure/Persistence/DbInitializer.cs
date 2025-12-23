using Bogus;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseControl.Infrastructure.Persistence;

public class DbInitializer(ExpenseControlDbContext context, ILogger<DbInitializer> logger)
{
	public async Task InitializeAsync()
	{
		try
		{
			if (context.Database.IsNpgsql())
				await context.Database.MigrateAsync();

			#if DEBUG
			await SeedDataAsync();
			#endif
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Erro na inicialização do banco.");
		}
	}

	private async Task SeedDataAsync()
	{
		if (await context.People.AnyAsync()) return;

		logger.LogInformation("Populando banco com Bogus...");

		#if DEBUG
		var faker = new Faker("pt_BR");

		var categories = new List<Category>
		{
			new("Salário", CategoryPurpose.Revenue),
			new("Mercado", CategoryPurpose.Expense),
			new("Lazer", CategoryPurpose.Expense)
		};
		await context.Categories.AddRangeAsync(categories);
		await context.SaveChangesAsync();

		var people = new List<Domain.Entities.Person>();
		for (int i = 0; i < 5; i++)
		{
			people.Add(new Domain.Entities.Person(faker.Name.FullName(), faker.Date.Past(30, DateTime.UtcNow.AddYears(-18))));
		}
		await context.People.AddRangeAsync(people);
		await context.SaveChangesAsync();

		var transactions = new List<Transaction>();
		for (int i = 0; i < 20; i++)
		{
			var person = faker.PickRandom(people);
			var category = faker.PickRandom(categories);
			var type = category.Purpose == CategoryPurpose.Revenue ? TransactionType.Revenue : TransactionType.Expense;

			if (person.Age < 18 && type == TransactionType.Revenue) continue;

			transactions.Add(new Transaction(
				faker.Commerce.ProductName(),
				decimal.Parse(faker.Commerce.Price(10, 500)),
				faker.Date.Recent(60),
				type,
				category,
				person.Id
			));
		}
		await context.Transactions.AddRangeAsync(transactions);
		await context.SaveChangesAsync();
		#endif
	}
}

