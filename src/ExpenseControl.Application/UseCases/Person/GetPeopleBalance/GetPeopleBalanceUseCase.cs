using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Domain.Interfaces.Services;

namespace ExpenseControl.Application.UseCases.Person.GetPeopleBalance;

public class GetPeopleBalanceUseCase(IPersonReportService service) : IGetPeopleBalanceUseCase
{
	public async Task<PersonBalanceResponse> ExecuteAsync()
	{
		var report = await service.GetReportAsync();

		var itemsDto = report.Items.Select(i =>
			new PersonBalanceItemResponse(i.Name, i.TotalRevenue, i.TotalExpense, i.Balance));

		return new PersonBalanceResponse(
			itemsDto,
			report.GrandTotalRevenue,
			report.GrandTotalExpense,
			report.GrandTotalBalance
		);
	}
}

