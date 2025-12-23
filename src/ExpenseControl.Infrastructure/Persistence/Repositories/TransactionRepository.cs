using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class TransactionRepository(ExpenseControlDbContext context) : ITransactionRepository
{
	public async Task AddAsync(Transaction transaction)
	{
		await context.Transactions.AddAsync(transaction);
	}

	public async Task<PaginatedResult<Transaction>> GetPaginatedAsync(int page, int pageSize, Guid? personId = null)
	{
		var query = context.Transactions
		.AsNoTracking()
		.Include(t => t.Category)
		.Include(t => t.Person)
		.OrderByDescending(t => t.Date)
		.AsQueryable(); 

		if (personId.HasValue)
			query = query.Where(t => t.PersonId == personId.Value);

		return await query.ToPaginatedResultAsync(page, pageSize);
	}

	public async Task<Transaction?> GetByIdAsync(Guid id)
	{
		return await context.Transactions
			.AsNoTracking() 
			.Include(t => t.Category) 
			.Include(t => t.Person)   
			.FirstOrDefaultAsync(t => t.Id == id);
	}

	public async Task<bool> HasAnyByCategoryIdAsync(Guid categoryId)
	{
		return await context.Transactions.AnyAsync(t => t.CategoryId == categoryId);
	}
	public void Update(Transaction transaction)
	{
		context.Transactions.Update(transaction);
	}

	public void Delete(Transaction transaction)
	{
		context.Transactions.Remove(transaction);
	}
}
