using ExpenseControl.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Infrastructure.Extensions;

public static class QueryableExtension
{
	public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
		this IQueryable<T> source,
		int pageNumber,
		int pageSize)
	{
		pageNumber = pageNumber < 1 ? 1 : pageNumber;
		pageSize = pageSize < 1 ? 10 : pageSize;

		var count = await source.CountAsync();

		var items = await source
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return new PaginatedResult<T>(items, pageNumber, pageSize, count);
	}
}

