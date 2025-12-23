using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository(ExpenseControlDbContext context) : IRefreshTokenRepository
{
	public async Task AddAsync(RefreshToken refreshToken)
	{
		await context.RefreshTokens.AddAsync(refreshToken);
	}

	public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
	{
		return await context.RefreshTokens
			.Include(rt => rt.User)
			.FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
	}

	public async Task RevokeAllByUserIdAsync(Guid userId)
	{
		await context.RefreshTokens
			.Where(rt => rt.UserId == userId && !rt.IsRevoked)
			.ExecuteUpdateAsync(calls => calls.SetProperty(rt => rt.IsRevoked, true));
	}
}

