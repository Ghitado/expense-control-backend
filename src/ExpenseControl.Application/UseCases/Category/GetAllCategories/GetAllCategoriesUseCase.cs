using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Category.GetAllCategories;

public class GetAllCategoriesUseCase(ICategoryRepository repository) : IGetAllCategoriesUseCase
{
	public async Task<PaginatedResult<CategoryResponse>> ExecuteAsync(int page, int size)
	{
		var result = await repository.GetAllAsync(page, size);

		var dtos = result.Items
			.Select(c => new CategoryResponse(c.Id, c.Description, c.Purpose))
			.ToList();

		return new PaginatedResult<CategoryResponse>(
			dtos, result.PageNumber, result.PageSize, result.TotalCount);
	}
}

