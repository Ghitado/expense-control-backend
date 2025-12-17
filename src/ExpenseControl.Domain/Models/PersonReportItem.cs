namespace ExpenseControl.Domain.Models;

public sealed record PersonReportItem(
	string Name,
	decimal TotalRevenue,
	decimal TotalExpense
)
{
	public decimal Balance => TotalRevenue - TotalExpense;
}
