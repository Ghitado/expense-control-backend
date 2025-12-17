using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Transaction;

public sealed record CreateTransactionRequest(
	string Description,
	decimal Amount,
	TransactionType Type,
	Guid CategoryId,
	Guid PersonId
);

