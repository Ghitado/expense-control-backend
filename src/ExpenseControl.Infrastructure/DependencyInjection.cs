using ExpenseControl.Domain.Interfaces;
using ExpenseControl.Domain.Interfaces.Repositories;
using ExpenseControl.Domain.Interfaces.Security.Cryptography;
using ExpenseControl.Domain.Interfaces.Security.Tokens;
using ExpenseControl.Infrastructure.Persistence;
using ExpenseControl.Infrastructure.Persistence.Repositories;
using ExpenseControl.Infrastructure.Security.Cryptography;
using ExpenseControl.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseControl.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddDatabase(configuration)
			.AddSecurity(configuration)
			.AddRepositories();
	}

	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ExpenseControlDbContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddScoped<DbInitializer>();

		return services;
	}

	private static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

		services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
		services.AddSingleton<ITokenService, JwtTokenService>();

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		return services
			.AddScoped<IUserRepository, UserRepository>()
			.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
			.AddScoped<IPersonRepository, PersonRepository>()
			.AddScoped<ICategoryRepository, CategoryRepository>()
			.AddScoped<ITransactionRepository, TransactionRepository>()
			.AddScoped<IReportRepository, ReportRepository>()
			.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}