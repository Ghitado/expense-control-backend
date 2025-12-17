using ExpenseControl.Domain.Models;

namespace ExpenseControl.Domain.Interfaces.Services;

public interface IPersonReportService
{
	Task<PersonReport> GetReportAsync();
}
