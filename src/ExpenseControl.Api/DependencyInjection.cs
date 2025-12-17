using ExpenseControl.Api.Middlewares;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ExpenseControl.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		return services
			.AddControllersWithConfiguration()
			.AddSwaggerConfiguration()
			.AddErrorHandling()
			.AddCorsConfiguration();
	}

	private static IServiceCollection AddControllersWithConfiguration(this IServiceCollection services)
	{
		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
		services.AddEndpointsApiExplorer();
		services.AddRouting(options => options.LowercaseUrls = true);
		return services;
	}

	private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Expense Control API",
				Version = "v1",
				Description = "API de controle de gastos residenciais para teste técnico."
			});

			// XML da API
			var xmlFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
			options.IncludeXmlComments(xmlPathApi);

			// XML da Application (DTOs e Enums)
			var xmlFileApp = "ExpenseControl.Application.xml";
			var xmlPathApp = Path.Combine(AppContext.BaseDirectory, xmlFileApp);
			if (File.Exists(xmlPathApp))
				options.IncludeXmlComments(xmlPathApp);
		});

		return services;
	}

	private static IServiceCollection AddErrorHandling(this IServiceCollection services)
	{
		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();
		return services;
	}

	private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAll",
				policy => policy.AllowAnyOrigin()
								.AllowAnyMethod()
								.AllowAnyHeader());
		});

		return services;
	}
}
