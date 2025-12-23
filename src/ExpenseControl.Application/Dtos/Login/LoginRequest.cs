namespace ExpenseControl.Application.Dtos.Login;

public sealed record LoginRequest(
	string Email,
	string Password
);

