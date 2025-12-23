namespace ExpenseControl.Domain.Entities;

public abstract class EntityBase
{
	public Guid Id { get; private set; }
	public DateTime CreatedAt { get; private set; }

	protected EntityBase()
	{
		Id = Guid.CreateVersion7();
		CreatedAt = DateTime.UtcNow;
	}
}

