namespace ExpenseControl.Domain.Models;

public sealed record CategoryReport
{
	public IReadOnlyCollection<CategoryReportItem> Items { get; }
	public decimal GrandTotalRevenue { get; }
	public decimal GrandTotalExpense { get; }

	public decimal GrandTotalBalance => GrandTotalRevenue - GrandTotalExpense;

	public CategoryReport(IEnumerable<CategoryReportItem> items)
	{
		var itemList = items.ToList();
		Items = itemList.AsReadOnly();

		GrandTotalRevenue = itemList.Sum(i => i.TotalRevenue);
		GrandTotalExpense = itemList.Sum(i => i.TotalExpense);
	}
}

