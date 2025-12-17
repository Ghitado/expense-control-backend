namespace ExpenseControl.Application.Dtos.Person;

public sealed record PersonBalanceResponse(
	IEnumerable<PersonBalanceItemResponse> Items,
	decimal GrandTotalRevenue,
	decimal GrandTotalExpense,
	decimal GrandTotalBalance
);

