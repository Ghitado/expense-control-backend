using ExpenseControl.Application.Dtos.Login;
using ExpenseControl.Application.Dtos.User;
using ExpenseControl.Application.Exceptions;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Interfaces.Security.Cryptography;
using ExpenseControl.Domain.Interfaces.Security.Tokens;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Login.DoLogin;

public sealed class LoginUserUseCase(
	IUserRepository userRepository,
	IRefreshTokenRepository refreshTokenRepository,
	IUnitOfWork unitOfWork,
	IPasswordHasher passwordHasher,
	ITokenService tokenService,
	IValidator<LoginRequest> validator)
	: ILoginUserUseCase
{
	public async Task<AuthResponse> ExecuteAsync(LoginRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		var user = await userRepository.GetByEmailAsync(request.Email);

		if (user is null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
			throw new InvalidCredentialsException();

		var accessToken = tokenService.CreateToken(user);

		var rawRefreshToken = tokenService.GenerateRefreshToken();

		var refreshTokenHash = tokenService.HashToken(rawRefreshToken);

		var refreshTokenEntity = new RefreshToken(
			user.Id,
			refreshTokenHash,
			DateTime.UtcNow.AddDays(7) 
		);

		await refreshTokenRepository.AddAsync(refreshTokenEntity);
		await unitOfWork.CommitAsync();

		return new AuthResponse(
			new UserResponse(user.Id, user.Email),
			accessToken,
			rawRefreshToken, 
			refreshTokenEntity.Expires
		);
	}
}

