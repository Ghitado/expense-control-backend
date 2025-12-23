using ExpenseControl.Application.Dtos.Person;

namespace ExpenseControl.Application.UseCases.Person.UpdatePerson;

public interface IUpdatePersonUseCase
{
	Task ExecuteAsync(Guid id, UpdatePersonRequest request);
}
