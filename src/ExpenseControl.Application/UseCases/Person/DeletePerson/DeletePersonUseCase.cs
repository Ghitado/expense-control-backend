using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace ExpenseControl.Application.UseCases.Person.DeletePerson;

public sealed class DeletePersonUseCase(
	IPersonRepository repository,
	IUnitOfWork unitOfWork,
	ILogger<DeletePersonUseCase> logger
	) : IDeletePersonUseCase
{
	public async Task ExecuteAsync(Guid id)
	{
		var person = await repository.GetByIdAsync(id);

		if (person is null)
			throw new ResourceNotFoundException(ApplicationErrors.Person.NotFound);

		repository.Delete(person);

		await unitOfWork.CommitAsync();

		logger.LogInformation(
			"Pessoa deletada com sucesso. ID: {PersonId} | Nome: {PersonName}",
			id,
			person.Name);
	}
}
