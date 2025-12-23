using ExpenseControl.Application.Dtos.Login;

namespace ExpenseControl.Application.UseCases.Login.DoLogin;

public interface ILoginUserUseCase
{
	Task<AuthResponse> ExecuteAsync(LoginRequest request);
}
