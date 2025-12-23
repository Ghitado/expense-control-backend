using ExpenseControl.Application.Dtos.Login;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Login.DoLogin;

public class LoginUserValidator : AbstractValidator<LoginRequest>
{
	public LoginUserValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Password).NotEmpty();
	}
}

