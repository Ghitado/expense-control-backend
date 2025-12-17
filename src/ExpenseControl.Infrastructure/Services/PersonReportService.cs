using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Interfaces.Services;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Services;

public class PersonReportService(ExpenseControlDbContext context) : IPersonReportService
{
	public async Task<PersonReport> GetReportAsync()
	{
		var items = await context.People
			.AsNoTracking()
			.Select(p => new PersonReportItem(
				p.Name,
				p.Transactions
					.Where(t => t.Type == TransactionType.Revenue)
					.Sum(t => t.Amount),

				p.Transactions
					.Where(t => t.Type == TransactionType.Expense)
					.Sum(t => t.Amount)
			))
			.ToListAsync();

		return new PersonReport(items);
	}
}

