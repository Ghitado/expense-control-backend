using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Constants;

namespace ExpenseControl.Domain.Entities;

public sealed class Category
{
	public Guid Id { get; private set; }
	public string Description { get; private set; } = string.Empty;
	public CategoryPurpose Purpose { get; private set; }

	private Category() { } 

	public Category(string description, CategoryPurpose purpose)
	{
		Validate(description);

		Id = Guid.CreateVersion7(); 
		Description = description;
		Purpose = purpose;
	}

	public bool IsCompatibleWith(TransactionType type)
	{
		if (Purpose is CategoryPurpose.Both) return true;
		return (int)Purpose == (int)type;
	}

	private static void Validate(string description)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new DomainException(DomainErrors.Category.DescriptionRequired);
	}
}
