using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Person.GetAllPeople;

public class GetAllPeopleUseCase(IPersonRepository repository) : IGetAllPeopleUseCase
{
	public async Task<PaginatedResult<PersonResponse>> ExecuteAsync(int page, int size)
	{
		var result = await repository.GetAllAsync(page, size);

		var dtos = result.Items
			.Select(p => new PersonResponse(p.Id, p.Name, p.Age))
			.ToList();

		return new PaginatedResult<PersonResponse>(
			dtos, result.PageNumber, result.PageSize, result.TotalCount);
	}
}

