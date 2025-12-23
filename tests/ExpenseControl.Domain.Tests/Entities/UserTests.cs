using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public sealed class UserTests
{
	[Fact]
	public void Constructor_ShouldCreateUser_WhenParamsAreValid()
	{
		// Arrange e Act
		var user = UserFactory.Create();

		// Assert
		Assert.NotNull(user);
		Assert.NotEqual(Guid.Empty, user.Id);
		Assert.NotNull(user.Email);
		Assert.NotNull(user.PasswordHash);
	}

	[Fact]
	public void Constructor_ShouldLowerCaseEmail()
	{
		// Arrange e Act
		var user = UserFactory.Create(email: "TESTE@EMAIL.COM");

		// Assert
		Assert.Equal("teste@email.com", user.Email);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void Constructor_ShouldThrowException_WhenEmailIsInvalid(string invalidEmail)
	{
		// Arrange, Act e Assert
		var exception = Assert.Throws<DomainException>(() => new Domain.Entities.User(invalidEmail, "hash123"));

		Assert.Equal(DomainErrors.User.EmailRequired, exception.Message);
	}
}

