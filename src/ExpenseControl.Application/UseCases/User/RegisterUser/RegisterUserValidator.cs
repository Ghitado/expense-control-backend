using ExpenseControl.Application.Dtos.User;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.User.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
	public RegisterUserValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("E-mail é obrigatório.")
			.EmailAddress().WithMessage("E-mail inválido.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Senha é obrigatória.")
			.MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.Password).WithMessage("As senhas não conferem.");
	}
}

