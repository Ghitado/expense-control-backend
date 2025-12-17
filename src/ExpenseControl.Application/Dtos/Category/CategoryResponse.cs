using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Category;

public sealed record CategoryResponse(
	Guid Id,
	string Description,
	CategoryPurpose Purpose
);

