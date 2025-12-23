using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.Dtos.Person;

public record UpdateCategoryRequest(
	string Name,
	CategoryPurpose? Purpose
);

