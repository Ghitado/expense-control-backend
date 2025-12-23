using ExpenseControl.Application.Dtos.User;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Tokens.RefreshToken;

public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
	public RefreshTokenValidator()
	{
		RuleFor(x => x.RefreshToken).NotEmpty();
	}
}

