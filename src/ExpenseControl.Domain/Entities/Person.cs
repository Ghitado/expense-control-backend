using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Errors;

namespace ExpenseControl.Domain.Entities;

public sealed class Person : EntityBase
{
	public string Name { get; private set; } = string.Empty;
	public DateTime BirthDate { get; private set; }
	public int Age => CalculateAge(BirthDate);

	private readonly List<Transaction> _transactions = [];
	public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

	private Person() { }

	public Person(string name, DateTime birthDate)
	{
		Validate(name, birthDate);

		Name = name;
		BirthDate = birthDate;
	}

	public void AddTransaction(Transaction transaction)
	{
		if (Age is < 18 && transaction.Type is TransactionType.Revenue)
			throw new DomainException(DomainErrors.Person.MinorCannotHaveRevenue);

		_transactions.Add(transaction);
	}

	public void Update(string name, DateTime birthDate, bool hasExistingRevenue)
	{
		Validate(name, birthDate);

		if (CalculateAge(birthDate) is < 18 && hasExistingRevenue)
			throw new DomainException(DomainErrors.Person.MinorCannotHaveRevenue);

		Name = name;
		BirthDate = birthDate;
	}

	private static void Validate(string name, DateTime birthDate)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException(DomainErrors.Person.NameRequired);

		if (birthDate.Date > DateTime.UtcNow.Date)
			throw new DomainException(DomainErrors.Person.BirthDateCannotBeFuture);
	}

	public static int CalculateAge(DateTime birthDate)
	{
		var today = DateTime.UtcNow;
		var age = today.Year - birthDate.Year;
		if (birthDate.Date > today.AddYears(-age)) age--;
		return age;
	}
}

