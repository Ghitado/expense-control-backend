using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Constants;

namespace ExpenseControl.Domain.Entities;

public sealed class Transaction
{
	public Guid Id { get; private set; }
	public string Description { get; private set; } = string.Empty;
	public decimal Amount { get; private set; }
	public TransactionType Type { get; private set; }
	public DateTime CreatedAt { get; private set; }

	public Guid CategoryId { get; private set; }
	public Category Category { get; private set; } = null!;

	public Guid PersonId { get; private set; }
	public Person Person { get; private set; } = null!;

	private Transaction() { }

	public Transaction(string description, decimal amount, TransactionType type, Category category, Guid personId)
	{
		Validate(description, amount);

		if (!category.IsCompatibleWith(type))
			throw new DomainException(DomainErrors.Transaction.CategoryIncompatible(category.Description, type));

		Id = Guid.CreateVersion7();
		Description = description;
		Amount = amount;
		Type = type;
		CategoryId = category.Id;
		PersonId = personId;
		CreatedAt = DateTime.UtcNow;
	}

	private static void Validate(string description, decimal amount)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new DomainException(DomainErrors.Transaction.DescriptionRequired);

		if (amount is <= 0)
			throw new DomainException(DomainErrors.Transaction.AmountMustBePositive);
	}
}
