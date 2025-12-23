namespace ExpenseControl.Domain.Models;

public sealed record ReportItem(
	Guid Id,
	string Label,
	decimal Revenue,
	decimal Expense)
{
	public decimal Balance => Revenue - Expense;
}

