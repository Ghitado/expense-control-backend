using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ExpenseControl.Application.UseCases.Transaction.CreateTransaction;

public class CreateTransactionUseCase(
	ITransactionRepository transactionRepository,
	IPersonRepository personRepository,
	ICategoryRepository categoryRepository,
	IUnitOfWork unitOfWork,
	IValidator<CreateTransactionRequest> validator,
	ILogger<CreateTransactionUseCase> logger
	) : ICreateTransactionUseCase
{
	public async Task<TransactionResponse> ExecuteAsync(CreateTransactionRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		var category = await categoryRepository.GetByIdAsync(request.CategoryId);
		if (category is null)
			throw new ResourceNotFoundException("Categoria não encontrada.");

		var person = await personRepository.GetByIdAsync(request.PersonId);
		if (person is null)
			throw new ResourceNotFoundException("Pessoa não encontrada.");

		var transaction = new Domain.Entities.Transaction(
			request.Description,
			request.Amount,
			request.Type,
			category,
			person.Id
		);

		person.AddTransaction(transaction);

		await transactionRepository.AddAsync(transaction);
		await unitOfWork.CommitAsync();

		logger.LogInformation(
			"Transação criada com sucesso. ID: {TransactionId} | Valor: {Amount} | Pessoa: {PersonName}",
			transaction.Id,
			transaction.Amount,
			person.Name);

		return new TransactionResponse(
			transaction.Id,
			transaction.Description,
			transaction.Amount,
			transaction.Type,
			category.Description, 
			person.Name,
			transaction.CreatedAt
		);
	}
}

