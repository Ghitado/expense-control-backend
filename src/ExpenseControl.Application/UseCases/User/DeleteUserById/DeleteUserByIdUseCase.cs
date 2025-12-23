using ExpenseControl.Application.Errors;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;

namespace ExpenseControl.Application.UseCases.User.DeleteUserById;

public sealed class DeleteUserByIdUseCase(
	IUserRepository userRepository,
	IUnitOfWork unitOfWork)
	: IDeleteUserByIdUseCase
{
	public async Task ExecuteAsync(Guid id)
	{
		var user = await userRepository.GetByIdAsync(id);

		if (user is null)
			throw new DomainException(ApplicationErrors.User.NotFound);

		userRepository.Delete(user);
		await unitOfWork.CommitAsync();
	}
}

