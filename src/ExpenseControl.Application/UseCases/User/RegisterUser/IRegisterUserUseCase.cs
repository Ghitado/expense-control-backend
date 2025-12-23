using ExpenseControl.Application.Dtos.User;

namespace ExpenseControl.Application.UseCases.User.RegisterUser;

public interface IRegisterUserUseCase
{
	Task<UserResponse> ExecuteAsync(RegisterUserRequest request);
}
