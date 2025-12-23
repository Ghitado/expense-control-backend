using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
	Task AddAsync(RefreshToken refreshToken);
	Task<RefreshToken?> GetByHashAsync(string tokenHash);
	Task RevokeAllByUserIdAsync(Guid userId);
}
