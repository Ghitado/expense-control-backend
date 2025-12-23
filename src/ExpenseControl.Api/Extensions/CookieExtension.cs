namespace ExpenseControl.Api.Extensions;

public static class CookieExtension
{
	private const string RefreshTokenCookieKey = "refreshToken";

	public static string? GetRefreshToken(this HttpRequest request)
	{
		return request.Cookies[RefreshTokenCookieKey];
	}

	public static void AddRefreshTokenCookie(this HttpResponse response, string refreshToken)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true, 
			SameSite = SameSiteMode.None, 
			Expires = DateTime.UtcNow.AddDays(7)
		};

		response.Cookies.Append(RefreshTokenCookieKey, refreshToken, cookieOptions);
	}

	public static void RemoveRefreshTokenCookie(this HttpResponse response)
	{
		response.Cookies.Delete(RefreshTokenCookieKey);
	}
}
