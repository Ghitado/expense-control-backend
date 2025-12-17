namespace ExpenseControl.Application.UseCases.Person.DeletePerson;

public interface IDeletePersonUseCase
{
	Task ExecuteAsync(Guid id);
}
