using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Application.Dtos.Reports;

namespace ExpenseControl.Application.UseCases.Reports.GetCategoriesBalance;

public interface IGetCategoriesBalanceUseCase
{
	Task<BalanceReportResponse> ExecuteAsync();
}
