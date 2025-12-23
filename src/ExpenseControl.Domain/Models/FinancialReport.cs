namespace ExpenseControl.Domain.Models;

public sealed record FinancialReport(
	IReadOnlyCollection<ReportItem> Items,
	decimal TotalRevenue,
	decimal TotalExpense)
{
	public decimal TotalBalance => TotalRevenue - TotalExpense;

	public static FinancialReport Create(IEnumerable<ReportItem> items)
	{
		var list = items.ToList().AsReadOnly();

		return new FinancialReport(
			list,
			list.Sum(i => i.Revenue),
			list.Sum(i => i.Expense));
	}
}

