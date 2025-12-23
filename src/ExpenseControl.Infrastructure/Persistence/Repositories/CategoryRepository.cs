using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Persistence.Repositories;

public sealed class CategoryRepository(ExpenseControlDbContext context) : ICategoryRepository
{
	public async Task AddAsync(Category category)
	{
		await context.Categories.AddAsync(category);
	}

	public async Task<Category?> GetByIdAsync(Guid id)
	{
		return await context.Categories.FindAsync(id);
	}

	public async Task<PaginatedResult<Category>> GetPaginatedAsync(int page, int pageSize)
	{
		return await context.Categories
			.AsNoTracking()
			.OrderBy(c => c.Name)
			.ToPaginatedResultAsync(page, pageSize);
	}

	public async Task<bool> HasTransactionsAsync(Guid categoryId)
	{
		return await context.Transactions.AnyAsync(t => t.CategoryId == categoryId);
	}

	public void Delete(Category category)
	{
		context.Categories.Remove(category);
	}
}

