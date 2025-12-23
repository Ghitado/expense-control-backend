using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class ReportRepository(ExpenseControlDbContext context) : IReportRepository
{
	public async Task<List<ReportItem>> GetPersonBalancesAsync()
	{
		return await context.People
			.AsNoTracking()
			.OrderBy(p => p.Name)
			.Select(p => new ReportItem(
				p.Id,
				p.Name,
				context.Transactions
					.Where(t => t.PersonId == p.Id && t.Type == TransactionType.Revenue)
					.Sum(t => t.Amount),
				context.Transactions
					.Where(t => t.PersonId == p.Id && t.Type == TransactionType.Expense)
					.Sum(t => t.Amount)
			))
			.ToListAsync();
	}

	public async Task<List<ReportItem>> GetCategoryBalancesAsync()
	{
		return await context.Categories
			.AsNoTracking()
			.OrderBy(c => c.Name)
			.Select(c => new ReportItem(
				c.Id,
				c.Name,
				context.Transactions
					.Where(t => t.CategoryId == c.Id && t.Type == TransactionType.Revenue)
					.Sum(t => t.Amount),
				context.Transactions
					.Where(t => t.CategoryId == c.Id && t.Type == TransactionType.Expense)
					.Sum(t => t.Amount)
			))
			.ToListAsync();
	}
}