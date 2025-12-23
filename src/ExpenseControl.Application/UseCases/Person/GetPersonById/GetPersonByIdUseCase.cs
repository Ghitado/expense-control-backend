using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Person.GetPersonById;

public sealed class GetPersonByIdUseCase(IPersonRepository repository) : IGetPersonByIdUseCase
{
	public async Task<PersonResponse> ExecuteAsync(Guid id)
	{
		var person = await repository.GetByIdAsync(id);

		if (person is null)
			throw new ResourceNotFoundException(ApplicationErrors.Person.NotFound);

		return new PersonResponse(person.Id, person.Name, person.BirthDate, person.Age);
	}
}

