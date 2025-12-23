using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionsPaginated;

public sealed class GetTransactionsPaginatedUseCase(ITransactionRepository repository) : IGetTransactionsPaginatedUseCase
{
	public async Task<PaginatedResult<TransactionResponse>> ExecuteAsync(int page, int pageSize, Guid? personId = null)
	{
		var pagedTransactions = await repository.GetPaginatedAsync(page, pageSize, personId);

		var dtos = pagedTransactions.Items.Select(t =>
			new TransactionResponse(
				t.Id,
				t.Description,
				t.Amount,
				t.Type,
				t.Category.Name,
				t.Person.Name,
				t.Date
			)
		).ToList();

		return new PaginatedResult<TransactionResponse>(
			dtos,
			pagedTransactions.PageNumber,
			pagedTransactions.PageSize,
			pagedTransactions.TotalCount
		);
	}
}

