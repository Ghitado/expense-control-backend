using ExpenseControl.Application.Dtos.Reports;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Reports.GetCategoriesBalance;

public sealed class GetCategoriesBalanceUseCase(IReportRepository repository) : IGetCategoriesBalanceUseCase
{
	public async Task<BalanceReportResponse> ExecuteAsync()
	{
		var items = await repository.GetCategoryBalancesAsync();

		var report = FinancialReport.Create(items);

		return new BalanceReportResponse(
			report.Items.Select(i => new ReportItemResponse(
				i.Id,
				i.Label,
				i.Revenue,
				i.Expense,
				i.Balance)).ToList(),
			report.TotalRevenue,
			report.TotalExpense,
			report.TotalBalance
		);
	}
}

