namespace ExpenseControl.Application.UseCases.Transaction.DeleteTransactionById;

public interface IDeleteTransactionUseCase
{
	Task ExecuteAsync(Guid id);
}
