using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Transaction.DeleteTransactionById;

public sealed class DeleteTransactionUseCase(
	ITransactionRepository repository,
	IUnitOfWork unitOfWork) : IDeleteTransactionUseCase
{
	public async Task ExecuteAsync(Guid id)
	{
		var transaction = await repository.GetByIdAsync(id);

		if (transaction is null)
			throw new ResourceNotFoundException(ApplicationErrors.Transaction.NotFound);

		repository.Delete(transaction);

		await unitOfWork.CommitAsync();
	}
}

