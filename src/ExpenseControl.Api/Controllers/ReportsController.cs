using ExpenseControl.Application.Dtos.Reports;
using ExpenseControl.Application.UseCases.Reports.GetCategoriesBalance;
using ExpenseControl.Application.UseCases.Reports.GetPeopleBalance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class ReportsController : ControllerBase
{
	/// <summary>
	/// Obtém o relatório de saldo financeiro agrupado por pessoas.
	/// </summary>
	/// <remarks>
	/// Este relatório agrega todas as transações de cada pessoa, calculando receitas totais, despesas e o saldo atual.
	/// Também fornece um total geral (Grand Total) para todo o sistema.
	/// </remarks>
	/// <param name="useCase">O caso de uso para executar a lógica de cálculo.</param>
	/// <returns>Um relatório detalhado contendo totais individuais e gerais.</returns>
	/// <response code="200">Retorna o relatório consolidado.</response>
	/// <response code="500">Se ocorrer um erro interno inesperado durante o cálculo.</response>
	[HttpGet("people-balance")]
	[ProducesResponseType(typeof(BalanceReportResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Relatório: Saldo por Pessoas",
		Description = "Calcula receita total, despesa e saldo para cada pessoa, mais um total geral do sistema.")]
	public async Task<IActionResult> GetPeopleBalance(
		[FromServices] IGetPeopleBalanceUseCase useCase)
	{
		var response = await useCase.ExecuteAsync();
		return Ok(response);
	}

	/// <summary>
	/// Obtém o relatório de saldo financeiro agrupado por categorias.
	/// </summary>
	/// <remarks>
	/// Este relatório agrega todas as transações de cada categoria, mostrando de onde o dinheiro está vindo (categorias de Receita) 
	/// e para onde está indo (categorias de Despesa).
	/// </remarks>
	/// <param name="useCase">O caso de uso para executar a lógica de cálculo.</param>
	/// <returns>Um relatório detalhado contendo totais baseados em categorias.</returns>
	/// <response code="200">Retorna o relatório consolidado.</response>
	/// <response code="500">Se ocorrer um erro interno inesperado durante o cálculo.</response>
	[HttpGet("categories-balance")]
	[ProducesResponseType(typeof(BalanceReportResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	[SwaggerOperation(
		Summary = "Relatório: Saldo por Categorias",
		Description = "Calcula receita total, despesa e saldo agrupados por categoria.")]
	public async Task<IActionResult> GetCategoriesBalance(
		[FromServices] IGetCategoriesBalanceUseCase useCase)
	{
		var response = await useCase.ExecuteAsync();
		return Ok(response);
	}

	// Futuro: (Background Job)
	// [HttpPost("generate-monthly-pdf")]
	// public IActionResult RequestPdfReport() { ... retorna 202 Accepted ... }
}