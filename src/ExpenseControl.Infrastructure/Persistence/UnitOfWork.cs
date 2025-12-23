using ExpenseControl.Domain.Interfaces;

namespace ExpenseControl.Infrastructure.Persistence;

public class UnitOfWork(ExpenseControlDbContext context) : IUnitOfWork
{
	public async Task CommitAsync() => await context.SaveChangesAsync();
}

