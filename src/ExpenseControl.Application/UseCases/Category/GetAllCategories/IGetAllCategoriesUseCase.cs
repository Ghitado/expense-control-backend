using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Category.GetAllCategories;

public interface IGetAllCategoriesUseCase
{
	Task<PaginatedResult<CategoryResponse>> ExecuteAsync(int page, int size);
}
