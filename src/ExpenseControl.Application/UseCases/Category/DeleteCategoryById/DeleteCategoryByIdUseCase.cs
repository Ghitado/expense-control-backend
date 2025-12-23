using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Category.DeleteCategoryById;

public sealed class DeleteCategoryByIdUseCase(
	ICategoryRepository repository,
	IUnitOfWork unitOfWork) : IDeleteCategoryByIdUseCase
{
	public async Task ExecuteAsync(Guid id)
	{
		var category = await repository.GetByIdAsync(id);
		
		if (category is null)
			throw new ResourceNotFoundException(ApplicationErrors.Category.NotFound);

		var isInUse = await repository.HasTransactionsAsync(id);
		if (isInUse)
			throw new DomainException(ApplicationErrors.Category.InUse);

		repository.Delete(category);
		await unitOfWork.CommitAsync();
	}
}
