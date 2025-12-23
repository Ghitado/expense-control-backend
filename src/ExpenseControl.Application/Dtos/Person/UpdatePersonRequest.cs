namespace ExpenseControl.Application.Dtos.Person;

public sealed record UpdatePersonRequest(
	string? Name,
	DateTime? BirthDate
);

