using ExpenseControl.Application.Dtos.Transaction;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionsByPerson;

public interface IGetTransactionsByPersonUseCase
{
	Task<IEnumerable<TransactionResponse>> ExecuteAsync(Guid personId);
}
