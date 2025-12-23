using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
	Task AddAsync(Transaction transaction);
	Task<Transaction?> GetByIdAsync(Guid id);
	Task<PaginatedResult<Transaction>> GetPaginatedAsync(int page, int pageSize, Guid? personId = null);
	Task<bool> HasAnyByCategoryIdAsync(Guid categoryId);
	void Update(Transaction transaction);
	void Delete(Transaction transaction);
}
