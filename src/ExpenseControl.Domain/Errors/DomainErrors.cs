using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Domain.Errors;

public static class DomainErrors
{
	public static class Person
	{
		public const string NameRequired = "O nome é obrigatório.";
		public const string BirthDateCannotBeFuture = "A data de nascimento não pode ser futura.";
		public const string MinorCannotHaveRevenue = "Menores de idade não podem registrar receitas, apenas despesas.";
	}

	public static class Transaction
	{
		public const string DescriptionRequired = "A descrição é obrigatória.";
		public const string AmountMustBePositive = "O valor deve ser positivo.";
		public const string InvalidDate = "A data da transação é inválida.";

		public static string CategoryIncompatible(string categoryName, TransactionType type)
			=> $"A categoria '{categoryName}' não é compatível com transações do tipo {type}.";
	}

	public static class Category
	{
		public const string DescriptionRequired = "A descrição da categoria é obrigatória.";
	}

	public static class User
	{
		public const string EmailRequired = "O e-mail é obrigatório.";
		public const string PasswordRequired = "A senha é obrigatória."; // Mensagem amigável, mesmo validando o hash
	}

	public static class RefreshToken
	{
		public const string UserIdRequired = "O ID do usuário é obrigatório.";
		public const string TokenHashRequired = "O hash do token é obrigatório.";
		public const string ExpirationMustBeFuture = "A data de expiração deve ser futura.";
		public const string InvalidToken = "Token inválido ou expirado.";
	}
}

