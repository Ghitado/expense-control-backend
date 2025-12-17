using ExpenseControl.Application.Dtos.Person;

namespace ExpenseControl.Application.UseCases.Person.GetPeopleBalance;

public interface IGetPeopleBalanceUseCase
{
	Task<PersonBalanceResponse> ExecuteAsync();
}