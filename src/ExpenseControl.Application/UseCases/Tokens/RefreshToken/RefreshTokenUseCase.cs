using ExpenseControl.Application.Dtos.Login;
using ExpenseControl.Application.Dtos.User;
using ExpenseControl.Application.Exceptions;
using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Interfaces.Security.Tokens;
using FluentValidation;

namespace ExpenseControl.Application.UseCases.Tokens.RefreshToken;

public sealed class RefreshTokenUseCase(
	IRefreshTokenRepository refreshTokenRepository,
	ITokenService tokenService,
	IUnitOfWork unitOfWork,
	IValidator<RefreshTokenRequest> validator)
	: IRefreshTokenUseCase
{
	public async Task<AuthResponse> ExecuteAsync(RefreshTokenRequest request)
	{
		await validator.ValidateAndThrowAsync(request);

		var requestHash = tokenService.HashToken(request.RefreshToken);

		var existingToken = await refreshTokenRepository.GetByHashAsync(requestHash);

		if (existingToken is null || !existingToken.IsActive)
			throw new InvalidCredentialsException();

		existingToken.Revoke();

		var newAccessToken = tokenService.CreateToken(existingToken.User!);

		var newRawRefreshToken = tokenService.GenerateRefreshToken();
		var newRefreshTokenHash = tokenService.HashToken(newRawRefreshToken);

		var newRefreshTokenEntity = new Domain.Entities.RefreshToken(
			existingToken.UserId,
			newRefreshTokenHash,
			DateTime.UtcNow.AddDays(7) 
		);

		await refreshTokenRepository.AddAsync(newRefreshTokenEntity);
		await unitOfWork.CommitAsync();

		return new AuthResponse(
			new UserResponse(existingToken.UserId, existingToken.User!.Email),
			newAccessToken,
			newRawRefreshToken,
			newRefreshTokenEntity.Expires
		);
	}
}

