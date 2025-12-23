using ExpenseControl.Application.Errors;

namespace ExpenseControl.Application.Exceptions;

public sealed class InvalidCredentialsException()
	: Exception(ApplicationErrors.Authentication.InvalidCredentials);

