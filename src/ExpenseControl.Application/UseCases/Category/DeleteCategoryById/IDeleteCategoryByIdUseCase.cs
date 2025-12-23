namespace ExpenseControl.Application.UseCases.Category.DeleteCategoryById;

public interface IDeleteCategoryByIdUseCase
{
	Task ExecuteAsync(Guid id);
}
