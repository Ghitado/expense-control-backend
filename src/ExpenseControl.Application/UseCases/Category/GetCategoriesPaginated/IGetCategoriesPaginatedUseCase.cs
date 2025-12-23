using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Category.GetCategoriesPaginated;

public interface IGetCategoriesPaginatedUseCase
{
	Task<PaginatedResult<CategoryResponse>> ExecuteAsync(int page, int size);
}
