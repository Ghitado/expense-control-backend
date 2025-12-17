using ExpenseControl.Domain.Constants;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public class TransactionTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreateTransaction()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);

		// Act
		var transaction = TransactionFactory.Create(TransactionType.Expense, category);

		// Assert
		Assert.NotNull(transaction);
		Assert.True(transaction.Amount > 0);
		Assert.Equal(category.Id, transaction.CategoryId);
	}

	[Fact]
	public void Constructor_WithInvalidAmount_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);
		var personId = Guid.CreateVersion7();
		var invalidAmount = -10m;

		// Act
		// Instanciação manual necessária para forçar o valor inválido que a Factory evita
		Action act = () => new Transaction("Lunch", invalidAmount, TransactionType.Expense, category, personId);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Transaction.AmountMustBePositive, exception.Message);
	}

	[Fact]
	public void Constructor_WithIncompatibleCategory_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);
		var type = TransactionType.Expense; 
		var personId = Guid.NewGuid();

		// Act
		Action act = () => new Transaction("Error", 100, type, category, personId);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		var expectedMessage = DomainErrors.Transaction.CategoryIncompatible(category.Description, type);

		Assert.Equal(expectedMessage, exception.Message);
	}
}