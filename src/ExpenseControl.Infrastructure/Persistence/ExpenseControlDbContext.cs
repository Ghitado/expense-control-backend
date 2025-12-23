using ExpenseControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseControl.Infrastructure.Persistence;

public class ExpenseControlDbContext(DbContextOptions<ExpenseControlDbContext> options) : DbContext(options) 
{ 
	public DbSet<Person> People { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(modelBuilder);
	}
}

