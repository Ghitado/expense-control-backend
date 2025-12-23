using ExpenseControl.Application.Dtos.User;

namespace ExpenseControl.Application.Dtos.Login;

public sealed record AuthResponse(
	UserResponse User,
	string AccessToken,
	string RefreshToken,
	DateTime Expiration
);

