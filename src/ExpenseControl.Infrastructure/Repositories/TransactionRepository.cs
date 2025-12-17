using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Repositories;

public class TransactionRepository(ExpenseControlDbContext context) : ITransactionRepository
{
	public async Task AddAsync(Transaction transaction)
	{
		await context.Transactions.AddAsync(transaction);
	}

	public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(Guid personId)
	{
		return await context.Transactions
			.Where(t => t.PersonId == personId)
			.Include(t => t.Category)
			.AsNoTracking()
			.ToListAsync();
	}
}
