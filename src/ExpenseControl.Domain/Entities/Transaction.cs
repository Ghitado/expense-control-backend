using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Errors;

namespace ExpenseControl.Domain.Entities;

public sealed class Transaction : EntityBase
{
	public string Description { get; private set; } = string.Empty;
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public TransactionType Type { get; private set; }

	public Guid CategoryId { get; private set; }
	public Category Category { get; private set; } = null!;

	public Guid PersonId { get; private set; }
	public Person Person { get; private set; } = null!;

	private Transaction() { }

	public Transaction(string description, decimal amount, DateTime date, TransactionType type, Category category, Guid personId)
	{
		Validate(description, amount, date);

		if (!category.IsCompatibleWith(type))
			throw new DomainException(DomainErrors.Transaction.CategoryIncompatible(category.Name, type));

		Description = description;
		Amount = amount;
		Date = date;
		Type = type;
		CategoryId = category.Id;
		PersonId = personId;
	}

	private static void Validate(string description, decimal amount, DateTime date)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new DomainException(DomainErrors.Transaction.DescriptionRequired);

		if (amount is <= 0)
			throw new DomainException(DomainErrors.Transaction.AmountMustBePositive);

		if (date == default)
			throw new DomainException(DomainErrors.Transaction.InvalidDate);
	}
}
