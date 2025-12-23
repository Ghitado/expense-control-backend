using ExpenseControl.Application.Dtos.Person;

namespace ExpenseControl.Application.UseCases.Person.GetPersonById;

public interface IGetPersonByIdUseCase
{
	Task<PersonResponse> ExecuteAsync(Guid id);
}
