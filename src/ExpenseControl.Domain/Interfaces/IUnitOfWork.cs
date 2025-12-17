namespace ExpenseControl.Domain.Interfaces;

public interface IUnitOfWork
{
	Task CommitAsync();
}
