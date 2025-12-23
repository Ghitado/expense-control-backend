using ExpenseControl.Api;
using ExpenseControl.Api.Extensions;
using ExpenseControl.Application;
using ExpenseControl.Infrastructure;
using ExpenseControl.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.ApplyMigrations();

	using (var scope = app.Services.CreateScope())
	{
		try
		{
			var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
			await initializer.InitializeAsync();
		}
		catch (Exception ex)
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "Ocorreu um erro ao rodar o Seeder no Program.cs");
		}
	}
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
