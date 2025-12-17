using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Transaction.GetTransactionsByPerson;

public class GetTransactionsByPersonUseCase(
	ITransactionRepository transactionRepository,
	IPersonRepository personRepository) : IGetTransactionsByPersonUseCase
{
	public async Task<IEnumerable<TransactionResponse>> ExecuteAsync(Guid personId)
	{
		var transactions = await transactionRepository.GetByPersonIdAsync(personId);

		var person = await personRepository.GetByIdAsync(personId);
		if (person is null)
			throw new ResourceNotFoundException("Pessoa não encontrada.");

		return transactions.Select(t => new TransactionResponse(
			t.Id,
			t.Description,
			t.Amount,
			t.Type,
			t.Category.Description, 
			person.Name,
			t.CreatedAt
		));
	}
}

