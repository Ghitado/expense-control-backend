using ExpenseControl.Application.Dtos.Person;

namespace ExpenseControl.Application.UseCases.Category.UpdateCategory;

public interface IUpdateCategoryUseCase
{
	Task ExecuteAsync(Guid id, UpdateCategoryRequest request);
}
