namespace ExpenseControl.Application.Errors;

public static class ApplicationErrors
{
	public static class Person
	{
		public const string NotFound = "Pessoa não encontrada.";
		public const string AlreadyExists = "Já existe uma pessoa cadastrada com este nome.";
	}

	public static class Transaction
	{
		public const string NotFound = "Transação não encontrada.";
	}

	public static class Category
	{
		public const string NotFound = "Categoria não encontrada.";
		public const string InUse = "Não é possível excluir esta categoria pois ela possui transações vinculadas.";
		public const string CannotChangePurpose = "Não é possível alterar a finalidade pois existem transações vinculadas.";
	}

	public static class User
	{
		public const string NotFound = "Usuário não encontrado.";
		public const string CannotDeleteSelf = "Você não pode excluir sua própria conta enquanto estiver logado."; 
		public const string EmailAlreadyExists = "Este e-mail já está em uso.";
	}

	public static class Authentication
	{
		public const string InvalidCredentials = "Credenciais inválidas.";
	}
}
