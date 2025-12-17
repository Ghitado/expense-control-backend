using ExpenseControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseControl.Api.Extensions;

public static class MigrationExtension
{
	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseControlDbContext>();

		var pendingMigrations = dbContext.Database.GetPendingMigrations();

		if (pendingMigrations.Any())
			dbContext.Database.Migrate();
	}
}
