namespace ExpenseControl.Application.UseCases.User.DeleteUserById;

public interface IDeleteUserByIdUseCase
{
	Task ExecuteAsync(Guid id);
}
