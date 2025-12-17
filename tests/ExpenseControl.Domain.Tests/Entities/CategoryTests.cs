using ExpenseControl.Domain.Constants;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public class CategoryTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateCategory()
	{
		// Act
		var category = CategoryFactory.Create();

		// Assert
		Assert.NotEqual(Guid.Empty, category.Id);
		Assert.False(string.IsNullOrWhiteSpace(category.Description));
	}

	[Fact]
	public void Constructor_WithEmptyDescription_ShouldThrowDomainException()
	{
		// Arrange
		var purpose = CategoryPurpose.Revenue;

		// Act
		Action act = () => new Category("", purpose);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Category.DescriptionRequired, exception.Message);
	}

	[Theory]
	[InlineData(CategoryPurpose.Revenue, TransactionType.Revenue, true)]
	[InlineData(CategoryPurpose.Revenue, TransactionType.Expense, false)]
	[InlineData(CategoryPurpose.Expense, TransactionType.Expense, true)]
	[InlineData(CategoryPurpose.Both, TransactionType.Revenue, true)]
	public void IsCompatibleWith_WithVariousScenarios_ShouldReturnExpectedResult(
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
}