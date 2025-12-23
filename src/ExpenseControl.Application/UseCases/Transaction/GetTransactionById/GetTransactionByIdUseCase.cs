using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionById;

public sealed class GetTransactionByIdUseCase(ITransactionRepository repository) : IGetTransactionByIdUseCase
{
	public async Task<TransactionResponse> ExecuteAsync(Guid id)
	{
		var transaction = await repository.GetByIdAsync(id);

		if (transaction is null)
			throw new ResourceNotFoundException(ApplicationErrors.Transaction.NotFound);

		return new TransactionResponse(
			transaction.Id,
			transaction.Description,
			transaction.Amount,
			transaction.Type,
			transaction.Category?.Name ?? string.Empty, 
			transaction.Person?.Name ?? string.Empty,   
			transaction.Date 
		);
	}
}

