using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Tests.Factories;

namespace ExpenseControl.Domain.Tests.Entities;

public class RefreshTokenTests
{
	[Fact]
	public void Constructor_ShouldCreateToken_WhenValid()
	{
		// Arrange e Act
		var token = RefreshTokenFactory.Create();

		// Assert
		Assert.NotEqual(Guid.Empty, token.Id);
		Assert.NotEqual(Guid.Empty, token.UserId);
		Assert.False(token.IsRevoked);
		Assert.True(token.IsActive);
	}

	[Fact]
	public void Revoke_ShouldMarkTokenAsRevoked()
	{
		// Arrange
		var token = RefreshTokenFactory.Create();

		// Act
		token.Revoke();

		// Assert
		Assert.True(token.IsRevoked);
		Assert.False(token.IsActive);
	}

	[Fact]
	public void Constructor_ShouldThrow_WhenUserIdIsEmpty()
	{
		// Arrange e Act
		var exception = Assert.Throws<DomainException>(() =>
			new RefreshToken(Guid.Empty, "hash", DateTime.UtcNow.AddDays(1)));

		// Assert
		Assert.Equal(DomainErrors.RefreshToken.UserIdRequired, exception.Message);
	}

	[Fact]
	public void IsActive_ShouldBeFalse_WhenExpired()
	{
		// Arrange
		var token = RefreshTokenFactory.Create();

		// Act
		var prop = typeof(RefreshToken).GetProperty(nameof(RefreshToken.Expires));
		prop!.SetValue(token, DateTime.UtcNow.AddMinutes(-10));

		// Assert
		Assert.False(token.IsActive);
	}
}

