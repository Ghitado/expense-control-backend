using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Security.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseControl.Infrastructure.Security.Tokens;

public sealed class JwtTokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
	public string CreateToken(User user)
	{
		var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Secret);

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new(JwtRegisteredClaimNames.Email, user.Email),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
        };

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(jwtSettings.Value.ExpiryMinutes),
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha512Signature),
			Issuer = jwtSettings.Value.Issuer,
			Audience = jwtSettings.Value.Audience
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}

	public string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);

		return Convert.ToBase64String(randomNumber)
			.Replace("+", "-").Replace("/", "_").Replace("=", "");
	}

	public string HashToken(string token)
	{
		using var sha256 = SHA256.Create();
		var bytes = Encoding.UTF8.GetBytes(token);
		var hash = sha256.ComputeHash(bytes);
		return Convert.ToBase64String(hash);
	}
}
