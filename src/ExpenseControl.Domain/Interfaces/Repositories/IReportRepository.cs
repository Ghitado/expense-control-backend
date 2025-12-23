using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface IReportRepository
{
	Task<List<ReportItem>> GetPersonBalancesAsync();
	Task<List<ReportItem>> GetCategoryBalancesAsync();
}
