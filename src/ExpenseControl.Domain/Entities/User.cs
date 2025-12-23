using ExpenseControl.Domain.Errors;
using ExpenseControl.Domain.Exceptions;

namespace ExpenseControl.Domain.Entities;

public sealed class User : EntityBase
{
	public string Email { get; private set; } = string.Empty;
	public string PasswordHash { get; private set; } = string.Empty;

	private User() { }

	public User(string email, string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new DomainException(DomainErrors.User.EmailRequired);

		if (string.IsNullOrWhiteSpace(passwordHash))
			throw new DomainException(DomainErrors.User.PasswordRequired);

		Email = email.ToLower(); 
		PasswordHash = passwordHash;
	}
}

