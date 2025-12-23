using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface IPersonRepository
{
	Task AddAsync(Person person);
	Task<Person?> GetByIdAsync(Guid id);
	Task<PaginatedResult<Person>> GetPaginatedAsync(int page, int pageSize);
	Task<bool> HasRevenueTransactionsAsync(Guid personId); 
	void Update(Person person);
	void Delete(Person person);
}
