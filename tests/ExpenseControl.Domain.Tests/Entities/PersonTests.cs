using Bogus;
using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public sealed class PersonTests
{
	private readonly Faker _faker = new("pt_BR");

	[Fact]
	public void Constructor_WithValidData_ShouldCreatePerson()
	{
		// Arrange e Act 
		var person = PersonFactory.Create();

		// Assert
		Assert.False(string.IsNullOrWhiteSpace(person.Name));
		Assert.NotEqual(default, person.BirthDate);
		Assert.NotEqual(Guid.Empty, person.Id);
		Assert.Empty(person.Transactions);
	}

	[Fact]
	public void Constructor_WithEmptyName_ShouldThrowDomainException()
	{
		// Arrange
		var birthDate = DateTime.UtcNow.AddYears(-20);

		// Act
		Action act = () => new Domain.Entities.Person("", birthDate);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.NameRequired, exception.Message);
	}

	[Fact]
	public void Constructor_WithFutureBirthDate_ShouldThrowDomainException()
	{
		// Arrange
		var futureDate = DateTime.UtcNow.AddDays(1);
		var name = _faker.Name.FullName();

		// Act
		Action act = () => new Domain.Entities.Person(name, futureDate);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.BirthDateCannotBeFuture, exception.Message);
	}

	[Fact]
	public void CalculateAge_BornToday_ShouldBeZero()
	{
		// Arrange
		var birthDate = DateTime.UtcNow.Date;

		// Act
		var age = Domain.Entities.Person.CalculateAge(birthDate);

		// Assert
		Assert.Equal(0, age);
	}

	[Fact]
	public void CalculateAge_BornExactly18YearsAgo_ShouldBe18()
	{
		// Arrange
		var birthDate = DateTime.UtcNow.Date.AddYears(-18);

		// Act
		var age = Domain.Entities.Person.CalculateAge(birthDate);

		// Assert
		Assert.Equal(18, age);
	}

	[Fact]
	public void CalculateAge_Born18YearsAgo_Tomorrow_ShouldBe17()
	{
		// Arrange
		var birthDate = DateTime.UtcNow.Date.AddYears(-18).AddDays(1);

		// Act
		var age = Domain.Entities.Person.CalculateAge(birthDate);

		// Assert
		Assert.Equal(17, age);
	}

	[Fact]
	public void CalculateAge_LeapYear_BornFeb29_ShouldCalculateCorrectly()
	{
		// Arrange
		var birthDate = new DateTime(2000, 2, 29, 0, 0, 0, DateTimeKind.Utc);

		// Act
		var age = Domain.Entities.Person.CalculateAge(birthDate);

		// Assert
		Assert.True(age > 0);
	}

	[Fact]
	public void AddTransaction_WithValidTransaction_ShouldAdd()
	{
		// Arrange
		var person = PersonFactory.Create();
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);
		var transaction = TransactionFactory.Create(TransactionType.Revenue, category, person.Id);

		// Act
		person.AddTransaction(transaction);

		// Assert
		Assert.Single(person.Transactions);
	}

	[Fact]
	public void AddTransaction_WithMinorAndRevenue_ShouldThrowDomainException()
	{
		// Arrange
		var person = PersonFactory.CreateMinor();
		var category = CategoryFactory.Create(CategoryPurpose.Revenue);
		var transaction = TransactionFactory.Create(TransactionType.Revenue, category, person.Id);

		// Act
		Action act = () => person.AddTransaction(transaction);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.MinorCannotHaveRevenue, exception.Message);
	}

	[Fact]
	public void AddTransaction_WithMinorAndExpense_ShouldSuccess()
	{
		// Arrange
		var person = PersonFactory.CreateMinor();
		var category = CategoryFactory.Create(CategoryPurpose.Expense);
		var transaction = TransactionFactory.Create(TransactionType.Expense, category, person.Id);

		// Act
		person.AddTransaction(transaction);

		// Assert
		Assert.Single(person.Transactions);
	}

	[Fact]
	public void Update_ChangingFromAdultToMinor_WithoutRevenue_ShouldSuccess()
	{
		// Arrange
		var person = PersonFactory.Create(); 
		var newName = _faker.Name.FullName();
		var newBirthDateChild = DateTime.UtcNow.AddYears(-10);

		// Act
		person.Update(newName, newBirthDateChild, hasExistingRevenue: false);

		// Assert
		Assert.Equal(10, person.Age);
		Assert.Equal(newName, person.Name);
	}

	[Fact]
	public void Update_ChangingFromAdultToMinor_WithRevenue_ShouldFail()
	{
		// Arrange
		var person = PersonFactory.Create(); 
		var initialName = person.Name;
		var initialAge = person.Age;
		var newBirthDateChild = DateTime.UtcNow.AddYears(-10);

		// Act
		Action act = () => person.Update(_faker.Name.FullName(), newBirthDateChild, hasExistingRevenue: true);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.MinorCannotHaveRevenue, exception.Message);
		Assert.Equal(initialName, person.Name);
		Assert.Equal(initialAge, person.Age);
	}

	[Fact]
	public void Update_AdultStayingAdult_WithRevenue_ShouldSuccess()
	{
		// Arrange
		var person = new Domain.Entities.Person("Adulto", DateTime.UtcNow.AddYears(-30));
		var newBirthDateStillAdult = DateTime.UtcNow.AddYears(-25);

		// Act
		person.Update("Adulto Editado", newBirthDateStillAdult, hasExistingRevenue: true);

		// Assert
		Assert.Equal(25, person.Age);
		Assert.Equal("Adulto Editado", person.Name);
	}

	[Fact]
	public void Update_WithInvalidName_ShouldThrowDomainException()
	{
		// Arrange
		var person = PersonFactory.Create();

		// Act
		Action act = () => person.Update("", person.BirthDate, false);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.NameRequired, exception.Message);
	}

	[Fact]
	public void Update_WithFutureDate_ShouldThrowDomainException()
	{
		// Arrange
		var person = PersonFactory.Create();
		var futureDate = DateTime.UtcNow.AddDays(1);

		// Act
		Action act = () => person.Update(person.Name, futureDate, false);

		// Assert
		var exception = Assert.Throws<DomainException>(act);
		Assert.Equal(DomainErrors.Person.BirthDateCannotBeFuture, exception.Message);
	}
}