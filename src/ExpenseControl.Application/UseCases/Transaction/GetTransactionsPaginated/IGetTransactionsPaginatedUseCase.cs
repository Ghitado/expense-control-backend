using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionsPaginated;

public interface IGetTransactionsPaginatedUseCase
{
	Task<PaginatedResult<TransactionResponse>> ExecuteAsync(int page, int pageSize, Guid? personId = null);
}
