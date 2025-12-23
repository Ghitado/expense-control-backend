using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Exceptions;

namespace ExpenseControl.Domain.Entities;

public sealed class Category : EntityBase
{
	public string Name { get; private set; } = string.Empty;
	public CategoryPurpose Purpose { get; private set; }

	private Category() { } 

	public Category(string name, CategoryPurpose purpose)
	{
		Validate(name);

		Name = name;
		Purpose = purpose;
	}

	public void UpdateName(string name)
	{
		Validate(name); 
		Name = name;
	}

	public void UpdatePurpose(CategoryPurpose purpose)
	{
		Purpose = purpose;
	}

	public bool IsCompatibleWith(TransactionType type)
	{
		if (Purpose is CategoryPurpose.Both) return true;
		return (int)Purpose == (int)type;
	}

	private static void Validate(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException(DomainErrors.Category.DescriptionRequired);
	}
}
