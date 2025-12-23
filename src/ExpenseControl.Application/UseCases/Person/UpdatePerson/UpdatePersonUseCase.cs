using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Person.UpdatePerson;

public sealed class UpdatePersonUseCase(
	IPersonRepository repository,
	IUnitOfWork unitOfWork) : IUpdatePersonUseCase
{
	public async Task ExecuteAsync(Guid id, UpdatePersonRequest request)
	{
		var person = await repository.GetByIdAsync(id);

		if (person is null)
			throw new ResourceNotFoundException(ApplicationErrors.Person.NotFound);

		var newName = request.Name ?? person.Name;
		var newBirthDate = request.BirthDate ?? person.BirthDate;

		bool hasRevenue = false;

		if (Domain.Entities.Person.CalculateAge(newBirthDate) is < 18)
			hasRevenue = await repository.HasRevenueTransactionsAsync(id);

		person.Update(newName, newBirthDate, hasRevenue);

		repository.Update(person);
		await unitOfWork.CommitAsync();
	}
}

