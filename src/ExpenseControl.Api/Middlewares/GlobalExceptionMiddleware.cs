using ExpenseControl.Application.Exceptions;
using ExpenseControl.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Middlewares;

public sealed class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		var problemDetails = exception switch
		{
			ValidationException ex => new ProblemDetails
			{
				Status = StatusCodes.Status400BadRequest,
				Title = "Erro de validação",
				Detail = "Verifique os campos informados.",
				Extensions = { ["errors"] = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
			},

			DomainException ex => new ProblemDetails
			{
				Status = StatusCodes.Status422UnprocessableEntity,
				Title = "Regra de negócio",
				Detail = ex.Message
			},

			ResourceNotFoundException ex => new ProblemDetails
			{
				Status = StatusCodes.Status404NotFound,
				Title = "Recurso não encontrado",
				Detail = ex.Message
			},

			AlreadyExistsException ex => new ProblemDetails
			{
				Status = StatusCodes.Status409Conflict,
				Title = "Conflito de dados",
				Detail = ex.Message
			},

			BusinessRuleException ex => new ProblemDetails
			{
				Status = StatusCodes.Status409Conflict,
				Title = "Conflito de operação",
				Detail = ex.Message
			},

			_ => LogAndCreateInternalError(exception)
		};

		problemDetails.Instance = httpContext.Request.Path;
		httpContext.Response.StatusCode = problemDetails.Status!.Value;

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
		return true;
	}

	private ProblemDetails LogAndCreateInternalError(Exception exception)
	{
		logger.LogError(exception, "Erro inesperado capturado pelo GlobalHandler.");

		return new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Title = "Erro interno",
			Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde."
		};
	}
}