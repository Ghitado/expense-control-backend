using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Interfaces.Services;
using ExpenseControl.Infrastructure.Persistence;
using ExpenseControl.Infrastructure.Repositories;
using ExpenseControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControl.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddDatabaseConnection(configuration)
			.AddRepositories()
			.AddServices();
	}

	private static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ExpenseControlDbContext>(options =>
			options.UseNpgsql(connectionString));

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		return services
			.AddScoped<IPersonRepository, PersonRepository>()
			.AddScoped<ICategoryRepository, CategoryRepository>()
			.AddScoped<ITransactionRepository, TransactionRepository>()
			.AddScoped<IUnitOfWork, UnitOfWork>();
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddScoped<IPersonReportService, PersonReportService>()
			.AddScoped<ICategoryReportService, CategoryReportService>();
	}
}
