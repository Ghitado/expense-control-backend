using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Person.GetPeoplePaginated;

public interface IGetPeoplePaginatedUseCase
{
	Task<PaginatedResult<PersonResponse>> ExecuteAsync(int page, int size);
}
