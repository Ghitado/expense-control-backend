using ExpenseControl.Api.Extensions;
using ExpenseControl.Application.Dtos.Login;
using ExpenseControl.Application.Dtos.User;
using ExpenseControl.Application.UseCases.Login.DoLogin;
using ExpenseControl.Application.UseCases.Tokens.RefreshToken;
using ExpenseControl.Application.UseCases.User.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
	/// <summary>
	/// Registra um novo usuário no sistema.
	/// </summary>
	/// <param name="useCase">O caso de uso de registro.</param>
	/// <param name="request">Dados do novo usuário (Email, Senha, Confirmação).</param>
	/// <returns>Dados do usuário criado (sem tokens).</returns>
	/// <response code="201">Usuário cadastrado com sucesso.</response>
	/// <response code="400">Dados inválidos (ex: senhas não conferem, email inválido).</response>
	/// <response code="409">Conflito: E-mail já cadastrado.</response>
	/// <response code="500">Erro interno do servidor.</response>
	[HttpPost("register")]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Registrar usuário",
		Description = "Cria uma nova conta de usuário. Não realiza login automático (stateless).")]
	public async Task<IActionResult> Register(
		[FromServices] IRegisterUserUseCase useCase,
		[FromBody] RegisterUserRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return Created(string.Empty, response);
	}

	/// <summary>
	/// Realiza o login e gera tokens de acesso.
	/// </summary>
	/// <param name="useCase">O caso de uso de login.</param>
	/// <param name="request">Credenciais (Email e Senha).</param>
	/// <returns>Access Token (JWT) e Refresh Token.</returns>
	/// <response code="200">Autenticação realizada com sucesso.</response>
	/// <response code="400">Dados de entrada inválidos.</response>
	/// <response code="401">Credenciais inválidas (Email ou Senha incorretos).</response>
	/// <response code="500">Erro interno do servidor.</response>
	[HttpPost("login")]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Realizar login",
		Description = "Autentica o usuário e retorna um JWT (Access Token) de curta duração e um Refresh Token opaco.")]
	public async Task<IActionResult> Login(
		[FromServices] ILoginUserUseCase useCase,
		[FromBody] LoginRequest request)
	{
		var response = await useCase.ExecuteAsync(request);

		Response.AddRefreshTokenCookie(response.RefreshToken);

		return Ok(response);
	}

	/// <summary>
	/// Renova o Access Token usando um Refresh Token válido.
	/// </summary>
	/// <param name="useCase">O caso de uso de renovação.</param>
	/// <param name="request">O Refresh Token atual (opaco).</param>
	/// <returns>Novos tokens de acesso e refresh.</returns>
	/// <response code="200">Tokens renovados com sucesso.</response>
	/// <response code="400">Token inválido ou ausente.</response>
	/// <response code="401">Token expirado ou revogado (necessário novo login).</response>
	[HttpPost("refresh-token")]
	[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[SwaggerOperation(
		Summary = "Renovar Token",
		Description = "Utiliza um Refresh Token válido para obter um novo par de Access Token e Refresh Token.")]
	public async Task<IActionResult> RefreshToken(
		[FromServices] IRefreshTokenUseCase useCase,
		[FromBody] RefreshTokenRequest? request)
	{
		var tokenStr = request?.RefreshToken ?? Request.GetRefreshToken();

		if (string.IsNullOrEmpty(tokenStr))
			return Unauthorized();

		var result = await useCase.ExecuteAsync(new RefreshTokenRequest(tokenStr));

		Response.AddRefreshTokenCookie(result.RefreshToken);

		return Ok(result);
	}

	/// <summary>
	/// Realiza o logout e limpa os cookies.
	/// </summary>
	/// <returns>Sem conteúdo.</returns>
	/// <response code="204">Logout realizado e cookie removido.</response>
	[HttpPost("logout")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[SwaggerOperation(
		Summary = "Realizar Logout",
		Description = "Instrui o navegador a remover o cookie do Refresh Token, encerrando a sessão persistente.")]
	public IActionResult Logout()
	{
		Response.RemoveRefreshTokenCookie();
		return NoContent();
	}
}
