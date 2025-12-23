using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Exceptions;

namespace ExpenseControl.Domain.Entities;

public sealed class RefreshToken : EntityBase
{
	public Guid UserId { get; private set; }
	public string TokenHash { get; private set; } = string.Empty;
	public DateTime Expires { get; private set; }
	public bool IsRevoked { get; private set; } = false;

	public User? User { get; private set; }

	private RefreshToken() { }

	public RefreshToken(Guid userId, string tokenHash, DateTime expires)
	{
		var expiresUtc = expires.ToUniversalTime();

		if (userId == Guid.Empty)
			throw new DomainException(DomainErrors.RefreshToken.UserIdRequired);

		if (string.IsNullOrWhiteSpace(tokenHash))
			throw new DomainException(DomainErrors.RefreshToken.TokenHashRequired);

		if (expiresUtc <= DateTime.UtcNow)
			throw new DomainException(DomainErrors.RefreshToken.ExpirationMustBeFuture);

		UserId = userId;
		TokenHash = tokenHash;
		Expires = expiresUtc;
		IsRevoked = false;
	}

	public bool IsActive => !IsRevoked && Expires > DateTime.UtcNow;

	public void Revoke() => IsRevoked = true;
}
