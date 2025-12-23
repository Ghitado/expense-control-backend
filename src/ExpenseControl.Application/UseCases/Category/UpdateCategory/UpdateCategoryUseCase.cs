using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.Errors;
using ExpenseControl.Application.Exceptions;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.Category.UpdateCategory;

public sealed class UpdateCategoryUseCase(
	ICategoryRepository categoryRepository,
	ITransactionRepository transactionRepository, 
	IUnitOfWork unitOfWork) : IUpdateCategoryUseCase
{
	public async Task ExecuteAsync(Guid id, UpdateCategoryRequest request)
	{
		var category = await categoryRepository.GetByIdAsync(id);

		if (category is null)
			throw new ResourceNotFoundException(ApplicationErrors.Category.NotFound);

		if (!string.IsNullOrWhiteSpace(request.Name))
			category.UpdateName(request.Name);

		if (request.Purpose.HasValue && request.Purpose != category.Purpose)
		{
			var hasTransactions = await transactionRepository.HasAnyByCategoryIdAsync(id);

			if (hasTransactions)
				throw new BusinessRuleException(ApplicationErrors.Category.CannotChangePurpose);

			category.UpdatePurpose(request.Purpose.Value);
		}

		await unitOfWork.CommitAsync();
	}
}