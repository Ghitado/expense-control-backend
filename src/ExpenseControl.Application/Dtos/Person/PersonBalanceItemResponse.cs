namespace ExpenseControl.Application.Dtos.Person;

public sealed record PersonBalanceItemResponse(
	string Name,
	decimal TotalRevenue,
	decimal TotalExpense,
	decimal Balance
);

