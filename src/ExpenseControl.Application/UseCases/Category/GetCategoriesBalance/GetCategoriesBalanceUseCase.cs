using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Domain.Interfaces.Services;

namespace ExpenseControl.Application.UseCases.Category.GetCategoriesBalance;

public class GetCategoriesBalanceUseCase(ICategoryReportService service) : IGetCategoriesBalanceUseCase
{
	public async Task<CategoryBalanceResponse> ExecuteAsync()
	{
		var report = await service.GetReportAsync();

		var itemsDto = report.Items.Select(i =>
			new CategoryBalanceItemResponse(i.Description, i.TotalRevenue, i.TotalExpense, i.Balance));

		return new CategoryBalanceResponse(
			itemsDto,
			report.GrandTotalRevenue,
			report.GrandTotalExpense,
			report.GrandTotalBalance
		);
	}
}

