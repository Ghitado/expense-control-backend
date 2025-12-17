using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Extensions;
using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Repositories;

public class PersonRepository(ExpenseControlDbContext context) : IPersonRepository
{
	public async Task AddAsync(Person person)
	{
		await context.People.AddAsync(person);
	}

	public async Task<Person?> GetByIdAsync(Guid id)
	{
		return await context.People
			.Include(p => p.Transactions)
			.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<PaginatedResult<Person>> GetAllAsync(int page, int pageSize)
	{
		return await context.People
			.AsNoTracking()
			.OrderBy(p => p.Name) 
			.ToPaginatedResultAsync(page, pageSize);
	}

	public void Delete(Person person)
	{
		context.People.Remove(person);
	}
}

