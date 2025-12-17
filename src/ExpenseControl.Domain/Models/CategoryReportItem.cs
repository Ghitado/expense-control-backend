namespace ExpenseControl.Domain.Models;

public sealed record CategoryReportItem(
	string Description,
	decimal TotalRevenue,
	decimal TotalExpense
)
{
	public decimal Balance => TotalRevenue - TotalExpense;
}

