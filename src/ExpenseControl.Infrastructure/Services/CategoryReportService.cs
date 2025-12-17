using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Interfaces.Services;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Services;

public class CategoryReportService(ExpenseControlDbContext context) : ICategoryReportService
{
	public async Task<CategoryReport> GetReportAsync()
	{
		var query =
			from category in context.Categories
			join transaction in context.Transactions
				on category.Id equals transaction.CategoryId
				into categoryTransactions 
			select new CategoryReportItem(
				category.Description,
				categoryTransactions.Where(t => t.Type == TransactionType.Revenue).Sum(t => t.Amount),
				categoryTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
			);

		var items = await query.AsNoTracking().ToListAsync();

		return new CategoryReport(items);
	}
}
