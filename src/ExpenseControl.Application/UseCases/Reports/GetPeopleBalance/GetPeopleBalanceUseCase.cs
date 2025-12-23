using ExpenseControl.Application.Dtos.Reports;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;

namespace ExpenseControl.Application.UseCases.Reports.GetPeopleBalance;

public sealed class GetPeopleBalanceUseCase(IReportRepository repository) : IGetPeopleBalanceUseCase
{
	public async Task<BalanceReportResponse> ExecuteAsync()
	{
		var items = await repository.GetPersonBalancesAsync();

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

