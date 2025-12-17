using ExpenseControl.Application.Dtos.Category;

namespace ExpenseControl.Application.UseCases.Category.CreateCategory;

public interface ICreateCategoryUseCase
{
	Task<CategoryResponse> ExecuteAsync(CreateCategoryRequest request);
}
