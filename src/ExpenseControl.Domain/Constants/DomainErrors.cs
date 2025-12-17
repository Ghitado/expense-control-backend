using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Domain.Constants;

public static class DomainErrors
{
	public static class Person
	{
		public const string NameRequired = "O nome é obrigatório.";
		public const string AgeCannotBeNegative = "A idade não pode ser negativa.";
		public const string MinorCannotHaveRevenue = "Menores de idade não podem registrar receitas, apenas despesas.";
	}

	public static class Transaction
	{
		public const string DescriptionRequired = "A descrição é obrigatória.";
		public const string AmountMustBePositive = "O valor deve ser positivo.";

		public static string CategoryIncompatible(string categoryName, TransactionType type)
			=> $"A categoria '{categoryName}' não é compatível com transações do tipo {type}.";
	}

	public static class Category
	{
		public const string DescriptionRequired = "A descrição da categoria é obrigatória.";
	}
}

