using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Infrastructure.Persistence;

namespace ExpenseControl.Infrastructure.Repositories;

public class UnitOfWork(ExpenseControlDbContext context) : IUnitOfWork
{
	public async Task CommitAsync() => await context.SaveChangesAsync();
}

