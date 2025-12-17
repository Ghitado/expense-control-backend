namespace ExpenseControl.Application.Dtos.Category;

public sealed record CategoryBalanceItemResponse(
	string Description,
	decimal TotalRevenue,
	decimal TotalExpense,
	decimal Balance
);

