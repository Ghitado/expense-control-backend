using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Models;
using ExpenseControl.Infrastructure.Extensions;
using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Repositories;

public class CategoryRepository(ExpenseControlDbContext context) : ICategoryRepository
{
	public async Task AddAsync(Category category)
	{
		await context.Categories.AddAsync(category);
	}

	public async Task<Category?> GetByIdAsync(Guid id)
	{
		return await context.Categories.FindAsync(id);
	}

	public async Task<PaginatedResult<Category>> GetAllAsync(int page, int pageSize)
	{
		return await context.Categories
			.AsNoTracking()
			.OrderBy(c => c.Description)
			.ToPaginatedResultAsync(page, pageSize);
	}
}

