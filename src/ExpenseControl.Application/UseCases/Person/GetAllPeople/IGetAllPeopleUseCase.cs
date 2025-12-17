using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Person.GetAllPeople;

public interface IGetAllPeopleUseCase
{
	Task<PaginatedResult<PersonResponse>> ExecuteAsync(int page, int size);
}
