using ExpenseControl.Application.Dtos.Category;
using ExpenseControl.Application.UseCases.Category.CreateCategory;
using ExpenseControl.Application.UseCases.Category.GetAllCategories;
using ExpenseControl.Application.UseCases.Category.GetCategoriesBalance;
using ExpenseControl.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExpenseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController : ControllerBase
{
	/// <summary>
	/// Creates a new category.
	/// </summary>
	/// <param name="useCase">Use case to execute the creation logic.</param>
	/// <param name="request">Data to create the category.</param>
	/// <returns>The created category.</returns>
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[SwaggerOperation(
		Summary = "Create category",
		Description = "Creates a new financial category (Revenue, Expense, or Both).")]
	public async Task<IActionResult> Create(
		[FromServices] ICreateCategoryUseCase useCase,
		[FromBody] CreateCategoryRequest request)
	{
		var response = await useCase.ExecuteAsync(request);
		return StatusCode(StatusCodes.Status201Created, response);
	}

	/// <summary>
	/// Retrieves a paginated list of categories.
	/// </summary>
	/// <param name="useCase">Use case to fetch categories.</param>
	/// <param name="page">Page number (starts at 1).</param>
	/// <param name="size">Number of items per page.</param>
	/// <returns>A paginated result containing the categories.</returns>
	[HttpGet]
	// ATENÇÃO: O tipo de retorno mudou de IEnumerable para PaginatedResult
	[ProducesResponseType(typeof(PaginatedResult<CategoryResponse>), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Get all categories (Paginated)",
		Description = "Returns a paginated list of registered categories with metadata.")]
	public async Task<IActionResult> GetAll(
		[FromServices] IGetAllCategoriesUseCase useCase,
		[FromQuery] int page = 1,
		[FromQuery] int size = 10)
	{
		var response = await useCase.ExecuteAsync(page, size);
		return Ok(response);
	}

	/// <summary>
	/// Retrieves the financial balance report for all categories.
	/// </summary>
	/// <param name="useCase">Use case to calculate the balance.</param>
	/// <returns>A detailed report containing individual and grand totals.</returns>
	[HttpGet("balance")]
	[ProducesResponseType(typeof(CategoryBalanceResponse), StatusCodes.Status200OK)]
	[SwaggerOperation(
		Summary = "Get financial balance",
		Description = "Returns the total revenue, expense, and balance for each category, along with the grand total.")]
	public async Task<IActionResult> GetBalance(
	[FromServices] IGetCategoriesBalanceUseCase useCase)
	{
		var response = await useCase.ExecuteAsync();
		return Ok(response);
	}
}