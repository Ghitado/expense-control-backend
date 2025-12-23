using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ExpenseControlDbContext context) : IUserRepository
{
	public async Task AddAsync(User user)
	{
		await context.Users.AddAsync(user);
	}

	public async Task<User?> GetByEmailAsync(string email)
	{
		return await context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());
	}

	public async Task<User?> GetByIdAsync(Guid id)
	{
		return await context.Users.FindAsync(id);
	}

	public async Task<bool> ExistsByEmailAsync(string email)
	{
		return await context.Users.AnyAsync(u => u.Email == email.ToLower());
	}

	public void Delete(User user)
	{
		context.Users.Remove(user);
	}
}

