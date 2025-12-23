using ExpenseControl.Application.Dtos.Transaction;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionById;

public interface IGetTransactionByIdUseCase
{
	Task<TransactionResponse> ExecuteAsync(Guid id);
}
