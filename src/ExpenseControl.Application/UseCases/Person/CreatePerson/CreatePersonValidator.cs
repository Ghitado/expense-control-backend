using ExpenseControl.Application.Dtos.Person;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Person.CreatePerson;

public sealed class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
	public CreatePersonValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("O nome é obrigatório.")
			.MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

		RuleFor(x => x.BirthDate)
			.NotEmpty().WithMessage("A data de nascimento é obrigatória.")
			.LessThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A data de nascimento não pode ser futura.");
	}
}

