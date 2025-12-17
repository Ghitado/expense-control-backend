namespace ExpenseControl.Application.Dtos.Person;

public sealed record CreatePersonRequest(
	string Name,
	int Age
);

