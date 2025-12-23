using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Category;

public sealed record CreateCategoryRequest(
	string Name,
	CategoryPurpose Purpose
);

