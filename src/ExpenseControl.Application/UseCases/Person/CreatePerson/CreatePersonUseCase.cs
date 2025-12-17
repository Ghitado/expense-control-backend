using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ExpenseControl.Application.UseCases.Person.CreatePerson;

public class CreatePersonUseCase(
	IPersonRepository repository,
	IUnitOfWork unitOfWork,
	IValidator<CreatePersonRequest> validator,
	ILogger<CreatePersonUseCase> logger) 
	: ICreatePersonUseCase
{
	public async Task<PersonResponse> ExecuteAsync(CreatePersonRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		var person = new Domain.Entities.Person(request.Name, request.Age);

		await repository.AddAsync(person);
		await unitOfWork.CommitAsync();

		logger.LogInformation(
			"Pessoa criada com sucesso. ID: {PersonId} | Nome: {Name}",
			person.Id,
			person.Name);

		return new PersonResponse(person.Id, person.Name, person.Age);
	}
}

