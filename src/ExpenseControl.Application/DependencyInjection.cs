using ExpenseControl.Application.UseCases.Category.CreateCategory;
using ExpenseControl.Application.UseCases.Category.GetAllCategories;
using ExpenseControl.Application.UseCases.Category.GetCategoriesBalance;
using ExpenseControl.Application.UseCases.Person.CreatePerson;
using ExpenseControl.Application.UseCases.Person.DeletePerson;
using ExpenseControl.Application.UseCases.Person.GetAllPeople;
using ExpenseControl.Application.UseCases.Person.GetPeopleBalance;
using ExpenseControl.Application.UseCases.Transaction.CreateTransaction;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionsByPerson;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseControl.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddValidation()
			.AddUseCases();
	}

	private static IServiceCollection AddValidation(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
	}

	private static IServiceCollection AddUseCases(this IServiceCollection services)
	{
		services
			.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>()
			.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>()
			.AddScoped<IGetCategoriesBalanceUseCase, GetCategoriesBalanceUseCase>();

		services
			.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>()
			.AddScoped<IGetAllPeopleUseCase, GetAllPeopleUseCase>()
			.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>()
			.AddScoped<IGetPeopleBalanceUseCase, GetPeopleBalanceUseCase>();

		services
			.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>()
			.AddScoped<IGetTransactionsByPersonUseCase, GetTransactionsByPersonUseCase>();

		return services;
	}
}

