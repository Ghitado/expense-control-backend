using ExpenseControl.Application.Dtos.Category;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Category.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
	public CreateCategoryValidator()
	{
		RuleFor(x => x.Description)
			.NotEmpty().WithMessage("A descrição é obrigatória.");

		RuleFor(x => x.Purpose)
			.IsInEnum().WithMessage("A finalidade é inválida.");
	}
}

