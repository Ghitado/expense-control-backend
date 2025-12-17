using ExpenseControl.Domain.Constants; 
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public class PersonTests
{
	[Fact]
	public void Constructor_WithValidData_ShouldCreatePerson()
	{
		// Arrange & Act
		var person = PersonFactory.Create();

		// Assert
		Assert.False(string.IsNullOrWhiteSpace(person.Name));
		Assert.True(person.Age >= 18);
		Assert.NotEqual(Guid.Empty, person.Id);
		Assert.Empty(person.Transactions);
	}

	[Fact]
	public void Constructor_WithEmptyName_ShouldThrowDomainException()
	{
		// Arrange
		var age = 30;

		// Act
		Action act = () => new Person("", age);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.NameRequired, exception.Message);
	}

	[Fact]
	public void AddTransaction_WithValidTransaction_ShouldAddTransaction()
	{
		// Arrange
		var person = PersonFactory.Create();
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);

		// A factory trata de criar uma transação válida com valores aleatórios
		var transaction = TransactionFactory.Create(TransactionType.Revenue, category, person.Id);

		// Act
		person.AddTransaction(transaction);

		// Assert
		Assert.Single(person.Transactions);
		Assert.Equal(transaction.Amount, person.Transactions.First().Amount);
	}

	[Fact]
	public void AddTransaction_WithMinorAndRevenue_ShouldThrowDomainException()
	{
		// Arrange
		var person = PersonFactory.CreateMinor(); // Factory específica para menores
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);
		var transaction = TransactionFactory.Create(TransactionType.Revenue, category, person.Id);

		// Act
		Action act = () => person.AddTransaction(transaction);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.MinorCannotHaveRevenue, exception.Message);
	}
}