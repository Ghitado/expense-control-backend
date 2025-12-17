namespace ExpenseControl.Domain.Models;

public sealed record PersonReport
{
	public IReadOnlyCollection<PersonReportItem> Items { get; }
	public decimal GrandTotalRevenue { get; }
	public decimal GrandTotalExpense { get; }

	// Regra de Negócio: Saldo Geral
	public decimal GrandTotalBalance => GrandTotalRevenue - GrandTotalExpense;

	public PersonReport(IEnumerable<PersonReportItem> items)
	{
		var itemList = items.ToList();
		Items = itemList.AsReadOnly();

		// O Domínio garante a consistência matemática do Total Geral
		GrandTotalRevenue = itemList.Sum(i => i.TotalRevenue);
		GrandTotalExpense = itemList.Sum(i => i.TotalExpense);
	}
}
