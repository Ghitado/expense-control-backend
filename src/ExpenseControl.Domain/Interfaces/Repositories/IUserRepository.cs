using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface IUserRepository
{
	Task AddAsync(User user);
	Task<User?> GetByEmailAsync(string email);
	Task<User?> GetByIdAsync(Guid id);
	Task<bool> ExistsByEmailAsync(string email);
	void Delete(User user);
}
