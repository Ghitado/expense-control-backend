namespace ExpenseControl.Application.Dtos.Category;

public sealed record CategoryBalanceResponse(
	IEnumerable<CategoryBalanceItemResponse> Items,
	decimal GrandTotalRevenue,
	decimal GrandTotalExpense,
	decimal GrandTotalBalance
);

