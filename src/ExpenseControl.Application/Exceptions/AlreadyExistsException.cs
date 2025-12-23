namespace ExpenseControl.Application.Exceptions;

public sealed class AlreadyExistsException(string message)
	: Exception(message);

