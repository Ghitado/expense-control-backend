using ExpenseControl.Application.Dtos.Category;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Category.CreateCategory;

public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
	public CreateCategoryValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("O nome é obrigatória.");

		RuleFor(x => x.Purpose)
			.IsInEnum().WithMessage("A finalidade é inválida.");
	}
}

