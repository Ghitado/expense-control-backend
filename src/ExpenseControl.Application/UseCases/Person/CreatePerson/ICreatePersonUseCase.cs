using ExpenseControl.Application.Dtos.Person;

namespace ExpenseControl.Application.UseCases.Person.CreatePerson;

public interface ICreatePersonUseCase
{
	Task<PersonResponse> ExecuteAsync(CreatePersonRequest request);
}
