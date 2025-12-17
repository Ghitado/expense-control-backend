using ExpenseControl.Application.Dtos.Person;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Person.CreatePerson;

public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
	public CreatePersonValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("O nome é obrigatório.")
			.MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

		RuleFor(x => x.Age)
			.GreaterThanOrEqualTo(0).WithMessage("A idade não pode ser negativa.");
	}
}

