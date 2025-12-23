using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class PersonRepository(ExpenseControlDbContext context) : IPersonRepository
{
	public async Task AddAsync(Person person)
	{
		await context.People.AddAsync(person);
	}

	public async Task<Person?> GetByIdAsync(Guid id)
	{
		return await context.People.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<PaginatedResult<Person>> GetPaginatedAsync(int page, int pageSize)
	{
		return await context.People
			.AsNoTracking()
			.OrderBy(p => p.Name) 
			.ToPaginatedResultAsync(page, pageSize);
	}

	public async Task<bool> HasRevenueTransactionsAsync(Guid personId)
	{
		return await context.Transactions
			.AsNoTracking()
			.AnyAsync(t => t.PersonId == personId && t.Type == TransactionType.Revenue);
	}

	public void Update(Person person)
	{
		context.People.Update(person);
	}

	public void Delete(Person person)
	{
		context.People.Remove(person);
	}
}

