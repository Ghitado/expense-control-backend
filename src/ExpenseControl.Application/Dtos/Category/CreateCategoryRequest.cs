using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Category;

public sealed record CreateCategoryRequest(
	/// <summary>
	/// Nome descritivo da categoria. Ex: Alimentação.
	/// </summary>
	string Description,

	/// <summary>
	/// Finalidade: Revenue (Receita), Expense (Despesa) ou Both (Ambos).
	/// </summary>
	CategoryPurpose Purpose
);

