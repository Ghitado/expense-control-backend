using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.UseCases.Category.CreateCategory;
using ExpenseControl.Application.UseCases.Category.DeleteCategoryById;
using ExpenseControl.Application.UseCases.Category.GetCategoriesPaginated;
using ExpenseControl.Application.UseCases.Category.UpdateCategory;
using ExpenseControl.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")] 
public sealed class CategoriesController : ControllerBase
{
	/// <summary>
	/// Cria uma nova categoria.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela lógica de criação.</param>
	/// <param name="request">Os dados da categoria (Descrição e Finalidade).</param>
	/// <returns>A categoria criada.</returns>
	/// <response code="201">Retorna a categoria criada.</response>
	/// <response code="400">Se a validação falhar (ex: descrição vazia).</response>
	/// <response code="500">Se ocorrer um erro interno inesperado.</response>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Criar categoria",
		Description = "Cria uma nova categoria financeira (Receita, Despesa ou Ambas).")]
	public async Task<IActionResult> Create(
		[FromServices] ICreateCategoryUseCase useCase,
		[FromBody] CreateCategoryRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return Created(string.Empty, response);
	}

	/// <summary>
	/// Obtém uma lista paginada de categorias.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela paginação.</param>
	/// <param name="page">Número da página (padrão é 1).</param>
	/// <param name="size">Quantidade de itens por página (padrão é 10).</param>
	/// <returns>Um resultado paginado contendo as categorias.</returns>
	/// <response code="200">Retorna a lista de categorias.</response>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<CategoryResponse>), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Obter categorias (Paginado)",
		Description = "Retorna uma lista paginada de categorias cadastradas ordenadas por nome.")]
	public async Task<IActionResult> GetPaginated(
		[FromServices] IGetCategoriesPaginatedUseCase useCase,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await useCase.ExecuteAsync(page, size);
		return Ok(response);
	}

	/// <summary>
	/// Atualiza parcialmente uma categoria.
	/// </summary>
	/// <remarks>
	/// Permite alterar o Nome ou a Finalidade da categoria.
	/// <br/>
	/// <b>Regra de Negócio:</b> Não é permitido alterar a Finalidade (Receita/Despesa) se a categoria já possuir transações vinculadas.
	/// </remarks>
	/// <param name="useCase">O caso de uso de atualização.</param>
	/// <param name="id">O UUID da categoria a ser atualizada.</param>
	/// <param name="request">Os novos dados (Nome e/ou Finalidade).</param>
	/// <returns>Sem conteúdo se for bem-sucedido.</returns>
	/// <response code="204">Atualização realizada com sucesso.</response>
	/// <response code="400">Dados inválidos (ex: nome vazio).</response>
	/// <response code="404">Categoria não encontrada.</response>
	/// <response code="409">Conflito de Regra de Negócio (ex: tentar mudar finalidade de categoria em uso).</response>
	[HttpPatch("{id:guid}")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[SwaggerOperation(
		Summary = "Atualizar categoria (Parcial)",
		Description = "Atualiza nome ou finalidade. Bloqueia mudança de finalidade se houver histórico financeiro.")]
	public async Task<IActionResult> Update(
		[FromServices] IUpdateCategoryUseCase useCase,
		[FromRoute] Guid id,
		[FromBody] UpdateCategoryRequest request)
	{
		await useCase.ExecuteAsync(id, request);
		return NoContent();
	}

	/// <summary>
	/// Exclui uma categoria pelo ID.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela exclusão.</param>
	/// <param name="id">O UUID da categoria a ser excluída.</param>
	/// <returns>Sem conteúdo se for bem-sucedido.</returns>
	/// <response code="204">Exclusão bem-sucedida.</response>
	/// <response code="404">Categoria não encontrada.</response>
	/// <response code="422">Se a categoria estiver em uso por transações (Regra de Negócio).</response>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
	[SwaggerOperation(
		Summary = "Excluir categoria",
		Description = "Remove uma categoria do sistema. Falha se existirem transações vinculadas a esta categoria.")]
	public async Task<IActionResult> Delete(
		[FromServices] IDeleteCategoryByIdUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.ExecuteAsync(id);
		return NoContent();
	}
}