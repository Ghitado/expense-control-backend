using ExpenseControl.Api.Middlewares;
using ExpenseControl.Infrastructure.Persistence;
using ExpenseControl.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace ExpenseControl.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddControllersWithConfiguration()
			.AddAuthenticationConfiguration(configuration)
			.AddSwaggerConfiguration() 
			.AddErrorHandling()
			.AddCorsConfiguration()
			.AddHealthCheckConfiguration();
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

	private static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

		if (jwtSettings is null)
			throw new ArgumentNullException(nameof(JwtSettings), $"A seção '{JwtSettings.SectionName}' não foi encontrada no appsettings.json.");

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),

				ClockSkew = TimeSpan.Zero
			};
		});

		return services;
	}

	private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
	{
		services.AddSwaggerGen(static options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Expense Control API",
				Version = "v1",
				Description = "API de controle de gastos residenciais."
			});

			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "Insira o token JWT desta maneira: Bearer {seu_token}",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});

			// XML Comments
			var xmlFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
			if (File.Exists(xmlPathApi)) options.IncludeXmlComments(xmlPathApi);

			var xmlFileApp = "ExpenseControl.Application.xml";
			var xmlPathApp = Path.Combine(AppContext.BaseDirectory, xmlFileApp);
			if (File.Exists(xmlPathApp)) options.IncludeXmlComments(xmlPathApp);
		});

		return services;
	}

	private static IServiceCollection AddErrorHandling(this IServiceCollection services)
	{
		services.AddExceptionHandler<GlobalExceptionMiddleware>();
		services.AddProblemDetails();
		return services;
	}

	private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
	{
		services.AddCors(options => options.AddDefaultPolicy(builder =>
		{
			builder
				.WithOrigins("futura vercel", "http://localhost:5173")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
		}));

		return services;
	}

	private static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
	{
		services.AddHealthChecks()
				.AddDbContextCheck<ExpenseControlDbContext>();

		return services;
	}
}
