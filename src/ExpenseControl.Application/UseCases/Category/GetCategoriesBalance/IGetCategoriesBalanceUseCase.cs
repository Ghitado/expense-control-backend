using ExpenseControl.Application.Dtos.Category;

namespace ExpenseControl.Application.UseCases.Category.GetCategoriesBalance;

public interface IGetCategoriesBalanceUseCase
{
	Task<CategoryBalanceResponse> ExecuteAsync();
}
