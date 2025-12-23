using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.UseCases.Person.CreatePerson;
using ExpenseControl.Application.UseCases.Person.DeletePerson;
using ExpenseControl.Application.UseCases.Person.GetPeoplePaginated;
using ExpenseControl.Application.UseCases.Person.GetPersonById;
using ExpenseControl.Application.UseCases.Person.UpdatePerson;
using ExpenseControl.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")] 
public sealed class PeopleController : ControllerBase
{
	/// <summary>
	/// Cria uma nova pessoa no sistema.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela lógica de criação.</param>
	/// <param name="request">Os dados da pessoa contendo Nome e Data de Nascimento.</param>
	/// <returns>A pessoa recém-criada com seu ID.</returns>
	/// <response code="201">Retorna a pessoa criada.</response>
	/// <response code="400">Se os dados forem inválidos (ex: nome vazio, data futura).</response>
	/// <response code="500">Se ocorrer um erro interno inesperado.</response>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Criar pessoa",
		Description = "Cadastra uma nova pessoa. Valida se o nome não está vazio e se a data de nascimento é válida.")]
	public async Task<IActionResult> Create(
		[FromServices] ICreatePersonUseCase useCase,
		[FromBody] CreatePersonRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	/// <summary>
	/// Obtém uma pessoa específica pelo seu identificador único.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela busca.</param>
	/// <param name="id">O UUID da pessoa.</param>
	/// <returns>Os detalhes da pessoa, incluindo a idade calculada.</returns>
	/// <response code="200">Retorna os detalhes da pessoa.</response>
	/// <response code="404">Se a pessoa com o ID especificado não for encontrada.</response>
	[HttpGet("{id:guid}")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Obter pessoa por ID",
		Description = "Busca informações detalhadas sobre uma pessoa específica, incluindo sua idade atual.")]
	public async Task<IActionResult> GetById(
		[FromServices] IGetPersonByIdUseCase useCase,
		[FromRoute] Guid id)
	{
		var response = await useCase.ExecuteAsync(id);
		return Ok(response);
	}

	/// <summary>
	/// Obtém uma lista paginada de pessoas cadastradas.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela paginação.</param>
	/// <param name="page">Número da página (padrão é 1).</param>
	/// <param name="size">Quantidade de itens por página (padrão é 10).</param>
	/// <returns>Uma lista paginada de pessoas.</returns>
	/// <response code="200">Retorna a lista de pessoas.</response>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<PersonResponse>), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Obter pessoas (Paginado)",
		Description = "Retorna uma lista de pessoas ordenada por nome com metadados de paginação (Total de itens, Total de páginas).")]
	public async Task<IActionResult> GetPaginated(
		[FromServices] IGetPeoplePaginatedUseCase useCase,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await useCase.ExecuteAsync(page, size);
		return Ok(response);
	}

	/// <summary>
	/// Atualiza parcialmente as informações de uma pessoa.
	/// </summary>
	/// <remarks>
	/// Este endpoint segue o padrão PATCH. Campos definidos como nulos na requisição serão ignorados.
	/// </remarks>
	/// <param name="useCase">O caso de uso responsável pela lógica de atualização.</param>
	/// <param name="id">O UUID da pessoa a ser atualizada.</param>
	/// <param name="request">Objeto contendo os campos a serem atualizados (Nome ou Data de Nascimento).</param>
	/// <returns>Sem conteúdo se for bem-sucedido.</returns>
	/// <response code="204">Atualização bem-sucedida.</response>
	/// <response code="400">Erro de validação (ex: formato de data inválido).</response>
	/// <response code="404">Pessoa não encontrada.</response>
	/// <response code="422">Violação de regra de negócio (ex: tentar definir idade de menor enquanto possui receitas).</response>
	[HttpPatch("{id:guid}")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
	[SwaggerOperation(
		Summary = "Atualizar pessoa (Parcial)",
		Description = "Atualiza o nome ou a data de nascimento. Valida se a nova idade viola a regra: 'Menores não podem ter receitas'.")]
	public async Task<IActionResult> Update(
		[FromServices] IUpdatePersonUseCase useCase,
		[FromRoute] Guid id,
		[FromBody] UpdatePersonRequest request)
	{
		await useCase.ExecuteAsync(id, request);
		return NoContent();
	}

	/// <summary>
	/// Exclui uma pessoa permanentemente.
	/// </summary>
	/// <param name="useCase">O caso de uso responsável pela exclusão.</param>
	/// <param name="id">O UUID da pessoa a ser excluída.</param>
	/// <returns>Sem conteúdo se for bem-sucedido.</returns>
	/// <response code="204">Exclusão bem-sucedida.</response>
	/// <response code="404">Pessoa não encontrada.</response>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Excluir pessoa",
		Description = "Remove uma pessoa do sistema. Aviso: Todas as transações associadas serão excluídas (Cascata).")]
	public async Task<IActionResult> Delete(
		[FromServices] IDeletePersonUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.ExecuteAsync(id);
		return NoContent();
	}
}