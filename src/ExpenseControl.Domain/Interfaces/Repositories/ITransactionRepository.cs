using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
	Task AddAsync(Transaction transaction);
	Task<IEnumerable<Transaction>> GetByPersonIdAsync(Guid personId);
}
