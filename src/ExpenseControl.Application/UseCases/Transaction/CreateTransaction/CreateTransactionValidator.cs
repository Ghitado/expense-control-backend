using ExpenseControl.Application.Dtos.Transaction;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Transaction.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
	public CreateTransactionValidator()
	{
		RuleFor(x => x.Description)
			.NotEmpty().WithMessage("A descrição é obrigatória.");

		RuleFor(x => x.Amount)
			.GreaterThan(0).WithMessage("O valor deve ser positivo.");

		RuleFor(x => x.Type)
			.IsInEnum().WithMessage("Tipo de transação inválido.");

		RuleFor(x => x.CategoryId)
			.NotEmpty().WithMessage("A categoria é obrigatória.");

		RuleFor(x => x.PersonId)
			.NotEmpty().WithMessage("A pessoa é obrigatória.");
	}
}

