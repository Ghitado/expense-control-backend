using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface IPersonRepository
{
	Task AddAsync(Person person);
	Task<Person?> GetByIdAsync(Guid id);
	Task<PaginatedResult<Person>> GetAllAsync(int page, int pageSize);
	void Delete(Person person);
}
