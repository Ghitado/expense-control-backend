using Bogus;
using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public sealed class TransactionTests
{
	private readonly Faker _faker = new("pt_BR");

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
		Assert.NotEqual(default, transaction.Date);
		Assert.Equal(category.Id, transaction.CategoryId);
	}

	[Fact]
	public void Constructor_WithZeroAmount_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);

		// Dados válidos gerados dinamicamente
		var validDescription = _faker.Commerce.ProductName();
		var validDate = _faker.Date.Recent();
		var validPersonId = Guid.NewGuid();

		// Zero (Fronteira exata), então ele é explícito.
		var zeroAmount = 0m;

		// Act
		Action act = () => new Transaction(
			validDescription,
			zeroAmount,
			validDate,
			TransactionType.Expense,
			category,
			validPersonId);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Transaction.AmountMustBePositive, exception.Message);
	}

	[Fact]
	public void Constructor_WithNegativeAmount_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);

		var negativeAmount = _faker.Random.Decimal(-1000, -0.01m);

		var validDescription = _faker.Commerce.ProductName();
		var validDate = _faker.Date.Recent();
		var validPersonId = Guid.NewGuid();

		// Act
		Action act = () => new Transaction(
			validDescription,
			negativeAmount,
			validDate,
			TransactionType.Expense,
			category,
			validPersonId);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Transaction.AmountMustBePositive, exception.Message);
	}

	[Fact]
	public void Constructor_WithEmptyDescription_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);

		// Dados válidos
		var validAmount = decimal.Parse(_faker.Commerce.Price(10, 100));
		var validDate = _faker.Date.Recent();

		var invalidDescription = "   ";

		// Act
		Action act = () => new Transaction(
			invalidDescription,
			validAmount,
			validDate,
			TransactionType.Expense,
			category,
			Guid.NewGuid());

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Transaction.DescriptionRequired, exception.Message);
	}

	[Fact]
	public void Constructor_WithDefaultDate_ShouldThrowDomainException()
	{
		// Arrange
		var category = CategoryFactory.Create(CategoryPurpose.Expense);

		var validDescription = _faker.Commerce.ProductName();
		var validAmount = decimal.Parse(_faker.Commerce.Price());

		// Data default (01/01/0001)
		var invalidDate = default(DateTime);

		// Act
		Action act = () => new Transaction(
			validDescription,
			validAmount,
			invalidDate,
			TransactionType.Expense,
			category,
			Guid.NewGuid());

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Transaction.InvalidDate, exception.Message);
	}

	[Fact]
	public void Constructor_WithIncompatibleCategory_ShouldThrowDomainException()
	{
		// Arrange
		// Cria cenário incompatível: Receita vs Despesa
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);
		var type = TransactionType.Expense;

		var validDescription = _faker.Commerce.ProductName();
		var validAmount = decimal.Parse(_faker.Commerce.Price());
		var validDate = _faker.Date.Recent();

		// Act
		Action act = () => new Transaction(
			validDescription,
			validAmount,
			validDate,
			type,
			category,
			Guid.NewGuid());

		// Assert
		var exception = Assert.Throws<DomainException>(act);

		var expectedMessage = DomainErrors.Transaction.CategoryIncompatible(category.Name, type);
		Assert.Equal(expectedMessage, exception.Message);
	}
}