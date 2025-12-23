namespace ExpenseControl.Application.Dtos.Person;

public sealed record PersonResponse(
	Guid Id,
	string Name,
	DateTime BirthDate,
	int Age
);

