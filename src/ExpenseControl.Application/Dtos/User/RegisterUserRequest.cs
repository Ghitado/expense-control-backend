namespace ExpenseControl.Application.Dtos.User;

public sealed record RegisterUserRequest(
	string Email,
	string Password,
	string ConfirmPassword
);

