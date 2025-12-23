using ExpenseControl.Application.UseCases.User.DeleteUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Authorize] 
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class UsersController : ControllerBase
{
	/// <summary>
	/// Exclui um usuário e revoga todos os acessos.
	/// </summary>
	/// <param name="useCase">O caso de uso de exclusão.</param>
	/// <param name="id">O UUID do usuário a ser excluído.</param>
	/// <returns>Sem conteúdo.</returns>
	/// <response code="204">Usuário excluído com sucesso.</response>
	/// <response code="401">Não autorizado (Token ausente ou inválido).</response>
	/// <response code="404">Usuário não encontrado.</response>
	/// <response code="500">Erro interno do servidor.</response>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Excluir usuário",
		Description = "Remove permanentemente a conta do usuário e revoga todos os refresh tokens ativos.")]
	public async Task<IActionResult> Delete(
		[FromServices] IDeleteUserByIdUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.ExecuteAsync(id);
		return NoContent();
	}
}
