using ExpenseControl.Domain.Enums;
using ExpenseControl.Domain.Exceptions;
using ExpenseControl.Domain.Constants;

namespace ExpenseControl.Domain.Entities;

public sealed class Person
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public int Age { get; private set; }

	private readonly List<Transaction> _transactions = [];
	public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

	private Person() { }

	public Person(string name, int age)
	{
		Validate(name, age);

		Id = Guid.CreateVersion7();
		Name = name;
		Age = age;
	}

	public void AddTransaction(Transaction transaction)
	{
		if (Age is < 18 && transaction.Type is TransactionType.Revenue)
			throw new DomainException(DomainErrors.Person.MinorCannotHaveRevenue);

		_transactions.Add(transaction);
	}

	public void Update(string name, int age)
	{
		Validate(name, age);
		Name = name;
		Age = age;
	}

	private static void Validate(string name, int age)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException(DomainErrors.Person.NameRequired);

		if (age is < 0)
			throw new DomainException(DomainErrors.Person.AgeCannotBeNegative);
	}
}

