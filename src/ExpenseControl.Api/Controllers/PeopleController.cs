using ExpenseControl.Application.Dtos.Person;
using ExpenseControl.Application.UseCases.Person.CreatePerson;
using ExpenseControl.Application.UseCases.Person.DeletePerson;
using ExpenseControl.Application.UseCases.Person.GetAllPeople;
using ExpenseControl.Application.UseCases.Person.GetPeopleBalance;
using ExpenseControl.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PeopleController : ControllerBase
{
	/// <summary>
	/// Creates a new person.
	/// </summary>
	/// <param name="useCase">Use case to execute the creation logic.</param>
	/// <param name="request">Data to create the person.</param>
	/// <returns>The created person.</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Create person",
		Description = "Creates a new person and returns the created resource.")]
	public async Task<IActionResult> Create(
		[FromServices] ICreatePersonUseCase useCase,
		[FromBody] CreatePersonRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
	}

	/// <summary>
	/// Retrieves a paginated list of people.
	/// </summary>
	/// <param name="useCase">Use case to fetch people.</param>
	/// <param name="page">Page number (starts at 1).</param>
	/// <param name="size">Number of items per page.</param>
	/// <returns>A paginated result containing the people.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(PaginatedResult<PersonResponse>), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Get all people (Paginated)",
		Description = "Returns a paginated list of registered people with metadata.")]
	public async Task<IActionResult> GetAll(
		[FromServices] IGetAllPeopleUseCase useCase,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await useCase.ExecuteAsync(page, size);
		return Ok(response);
	}

	/// <summary>
	/// Deletes a person by ID.
	/// </summary>
	/// <param name="useCase">Use case to execute the deletion.</param>
	/// <param name="id">The unique identifier of the person.</param>
	/// <returns>No content if successful.</returns>
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Delete person",
		Description = "Deletes a person and all their associated transactions.")]
	public async Task<IActionResult> Delete(
		[FromServices] IDeletePersonUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.ExecuteAsync(id);
		return NoContent();
	}

	/// <summary>
	/// Retrieves the financial balance report for all people.
	/// </summary>
	/// <param name="useCase">Use case to calculate the balance.</param>
	/// <returns>A detailed report containing individual and grand totals.</returns>
	[HttpGet("balance")]
	[ProducesResponseType(typeof(PersonBalanceResponse), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Get financial balance",
		Description = "Returns the total revenue, expense, and balance for each person, along with the grand total.")]
	public async Task<IActionResult> GetBalance(
		[FromServices] IGetPeopleBalanceUseCase useCase)
	{
		var response = await useCase.ExecuteAsync();
		return Ok(response);
	}
}