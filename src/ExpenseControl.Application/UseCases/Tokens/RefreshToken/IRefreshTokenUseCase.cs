using ExpenseControl.Application.Dtos.Login;
using ExpenseControl.Application.Dtos.User;

namespace ExpenseControl.Application.UseCases.Tokens.RefreshToken;

public interface IRefreshTokenUseCase
{
	Task<AuthResponse> ExecuteAsync(RefreshTokenRequest request);
}