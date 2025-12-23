using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public sealed class CategoryTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateCategory()
	{
		// Arrange e Act
		var category = CategoryFactory.Create();

		// Assert
		Assert.NotEqual(Guid.Empty, category.Id);
		Assert.False(string.IsNullOrWhiteSpace(category.Name));
		Assert.NotEqual(default, category.CreatedAt);
	}

	[Fact]
	public void Constructor_WithEmptyName_ShouldThrowDomainException()
	{
		// Arrange e Act
		Action act = () => new Category("", CategoryPurpose.Revenue);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Category.DescriptionRequired, exception.Message);
	}

	[Theory]
	[InlineData(CategoryPurpose.Revenue, TransactionType.Revenue, true)]
	[InlineData(CategoryPurpose.Revenue, TransactionType.Expense, false)]
	[InlineData(CategoryPurpose.Expense, TransactionType.Expense, true)]
	[InlineData(CategoryPurpose.Expense, TransactionType.Revenue, false)]
	[InlineData(CategoryPurpose.Both, TransactionType.Revenue, true)]
	[InlineData(CategoryPurpose.Both, TransactionType.Expense, true)]
	public void IsCompatibleWith_Matrix_ShouldReturnExpectedResult(
		CategoryPurpose purpose,
		TransactionType type,
		bool expected)
	{
		// Arrange
		var category = CategoryFactory.Create(purpose);

		// Act
		var result = category.IsCompatibleWith(type);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void UpdateName_WithValidData_ShouldUpdateName()
	{
		// Arrange
		var category = CategoryFactory.Create();
		var newName = "Nova Categoria Atualizada";

		// Act
		category.UpdateName(newName);

		// Assert
		Assert.Equal(newName, category.Name);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void UpdateName_WithInvalidData_ShouldThrowDomainException(string invalidName)
	{
		// Arrange
		var category = CategoryFactory.Create();

		// Act
		Action act = () => category.UpdateName(invalidName);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Category.DescriptionRequired, exception.Message);
	}

	[Fact]
	public void UpdatePurpose_ShouldUpdatePurpose()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Revenue); 
		var newPurpose = CategoryPurpose.Expense; 

		// Act
		category.UpdatePurpose(newPurpose);

		// Assert
		Assert.Equal(newPurpose, category.Purpose);
	}
}
}