using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Transaction;

public sealed record TransactionResponse(
	Guid Id,
	string Description,
	decimal Amount,
	TransactionType Type,
	string CategoryName, 
	string PersonName,
	DateTime CreatedAt 
);

