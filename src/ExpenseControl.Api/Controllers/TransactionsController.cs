using ExpenseControl.Application.Dtos.Transaction;
using ExpenseControl.Application.UseCases.Transaction.CreateTransaction;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionsByPerson;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionsController : ControllerBase
{
	/// <summary>
	/// Creates a new transaction.
	/// </summary>
	/// <param name="useCase">Use case to execute the creation logic.</param>
	/// <param name="request">Data to create the transaction.</param>
	/// <returns>The created transaction.</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
	[SwaggerOperation(
		Summary = "Create transaction",
		Description = "Creates a new financial transaction linked to a person and a category.")]
	public async Task<IActionResult> Create(
		[FromServices] ICreateTransactionUseCase useCase,
		[FromBody] CreateTransactionRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return StatusCode(StatusCodes.Status201Created, response);
	}

	/// <summary>
	/// Retrieves transactions for a specific person.
	/// </summary>
	/// <param name="useCase">Use case to fetch transactions.</param>
	/// <param name="personId">The unique identifier of the person.</param>
	/// <returns>A list of transactions belonging to the person.</returns>
	[HttpGet("person/{personId:guid}")]
	[ProducesResponseType(typeof(IEnumerable<TransactionResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get transactions by person",
		Description = "Returns a list of transactions belonging to a specific person.")]
	public async Task<IActionResult> GetByPerson(
		[FromServices] IGetTransactionsByPersonUseCase useCase,
		[FromRoute] Guid personId)
	{
		var response = await useCase.ExecuteAsync(personId);
		return Ok(response);
	}
}