using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Services;

public interface ICategoryReportService
{
	Task<CategoryReport> GetReportAsync();
}
