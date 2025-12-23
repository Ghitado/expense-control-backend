using ExpenseControl.Application.Dtos.Reports;

namespace ExpenseControl.Application.UseCases.Reports.GetPeopleBalance;

public interface IGetPeopleBalanceUseCase
{
	Task<BalanceReportResponse> ExecuteAsync();
}