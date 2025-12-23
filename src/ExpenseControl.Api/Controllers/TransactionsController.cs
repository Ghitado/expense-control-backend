using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Application.UseCases.Transaction.CreateTransaction;
using ExpenseControl.Application.UseCases.Transaction.DeleteTransactionById;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionById;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionsPaginated;
using ExpenseControl.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class TransactionsController : ControllerBase
{
	/// <summary>
	/// Cria uma nova transação.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela lógica de criação.</param>
	/// <param name="request">Os dados para criar a transação.</param>
	/// <returns>A transação criada.</returns>
	/// <response code="201">Retorna a transação criada.</response>
	/// <response code="400">Se a validação falhar (ex: valor negativo, data inválida).</response>
	/// <response code="404">Se a Pessoa ou Categoria referenciada não existir.</response>
	/// <response code="422">Se regras de negócio forem violadas (ex: Categoria de Despesa usada em Transação de Receita).</response>
	/// <response code="500">Se ocorrer um erro interno inesperado.</response>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Criar transação",
		Description = "Cria uma nova transação financeira vinculada a uma pessoa e uma categoria. Valida a compatibilidade entre o Tipo de Transação e a Finalidade da Categoria.")]
	public async Task<IActionResult> Create(
		[FromServices] ICreateTransactionUseCase useCase,
		[FromBody] CreateTransactionRequest request)
	{
		var response = await useCase.ExecuteAsync(request);

		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	/// <summary>
	/// Obtém uma lista paginada de transações.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável por buscar as transações.</param>
	/// <param name="page">Número da página (padrão é 1).</param>
	/// <param name="size">Quantidade de itens por página (padrão é 10).</param>
	/// <param name="personId">Filtro opcional para recuperar transações de uma pessoa específica.</param>
	/// <returns>Um resultado paginado contendo as transações.</returns>
	/// <response code="200">Retorna a lista de transações.</response>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<TransactionResponse>), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Obter transações (Paginado)",
		Description = "Retorna uma lista paginada de transações cadastradas com metadados. Suporta filtragem por ID da Pessoa.")]
	public async Task<IActionResult> GetPaginated(
		[FromServices] IGetTransactionsPaginatedUseCase useCase,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10,
		[FromQuery] Guid? personId = null)
	{
		var response = await useCase.ExecuteAsync(page, size, personId);
		return Ok(response);
	}

	/// <summary>
	/// Obtém uma transação específica pelo ID.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela busca.</param>
	/// <param name="id">O identificador único da transação.</param>
	/// <returns>Os detalhes da transação.</returns>
	/// <response code="200">Retorna os detalhes da transação.</response>
	/// <response code="404">Se a transação não for encontrada.</response>
	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Obter transação por ID",
		Description = "Busca informações detalhadas sobre uma transação específica.")]
	public async Task<IActionResult> GetById(
		[FromServices] IGetTransactionByIdUseCase useCase,
		[FromRoute] Guid id)
	{
		var response = await useCase.ExecuteAsync(id);
		return Ok(response);
	}

	/// <summary>
	/// Exclui uma transação pelo ID.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela exclusão.</param>
	/// <param name="id">O identificador único da transação.</param>
	/// <returns>Sem conteúdo se for bem-sucedido.</returns>
	/// <response code="204">Exclusão bem-sucedida.</response>
	/// <response code="404">Transação não encontrada.</response>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Excluir transação",
		Description = "Remove uma transação permanentemente do sistema.")]
	public async Task<IActionResult> Delete(
		[FromServices] IDeleteTransactionUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.ExecuteAsync(id);
		return NoContent();
	}
}