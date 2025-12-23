using ExpenseControl.Application.Dtos.User;
using ExpenseControl.Application.Errors;
using ExpenseControl.Application.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Interfaces.Security.Cryptography;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.User.RegisterUser;

public sealed class RegisterUserUseCase(
	IUserRepository userRepository,
	IUnitOfWork unitOfWork,
	IPasswordHasher passwordHasher,
	IValidator<RegisterUserRequest> validator)
	: IRegisterUserUseCase
{
	public async Task<UserResponse> ExecuteAsync(RegisterUserRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		if (await userRepository.ExistsByEmailAsync(request.Email))
			throw new AlreadyExistsException(ApplicationErrors.User.EmailAlreadyExists);

		var passwordHash = passwordHasher.HashPassword(request.Password);

		var user = new Domain.Entities.User(request.Email, passwordHash);

		await userRepository.AddAsync(user);
		await unitOfWork.CommitAsync();

		return new UserResponse(user.Id, user.Email);
	}
}

