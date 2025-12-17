using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ExpenseControl.Application.UseCases.Category.CreateCategory;

public class CreateCategoryUseCase(
	ICategoryRepository repository,
	IUnitOfWork unitOfWork,
	IValidator<CreateCategoryRequest> validator,
	ILogger<CreateCategoryUseCase> logger
	) : ICreateCategoryUseCase
{
	public async Task<CategoryResponse> ExecuteAsync(CreateCategoryRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		var category = new Domain.Entities.Category(request.Description, request.Purpose);

		await repository.AddAsync(category);
		await unitOfWork.CommitAsync();

		logger.LogInformation(
			"Categoria criada com sucesso. ID: {CategoryId} | Descrição: {Description}",
			category.Id,
			category.Description);

		return new CategoryResponse(category.Id, category.Description, category.Purpose);
	}
}

