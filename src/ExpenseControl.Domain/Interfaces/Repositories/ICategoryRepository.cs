using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
	Task AddAsync(Category category);
	Task<Category?> GetByIdAsync(Guid id);
	Task<PaginatedResult<Category>> GetAllAsync(int page, int pageSize);
}
