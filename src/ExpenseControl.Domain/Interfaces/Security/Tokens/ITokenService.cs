using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Domain.Interfaces.Security.Tokens;

public interface ITokenService
{
	string CreateToken(User user);
	string GenerateRefreshToken();
	string HashToken(string token);
}
