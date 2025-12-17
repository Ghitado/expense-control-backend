using ExpenseControl.Application.Dtos.Transaction;

namespace ExpenseControl.Application.UseCases.Transaction.CreateTransaction;

public interface ICreateTransactionUseCase
{
	Task<TransactionResponse> ExecuteAsync(CreateTransactionRequest request);
}
