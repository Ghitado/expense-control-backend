using ExpenseControl.Application.UseCases.Category.CreateCategory;
using ExpenseControl.Application.UseCases.Category.DeleteCategoryById;
using ExpenseControl.Application.UseCases.Category.GetCategoriesPaginated;
using ExpenseControl.Application.UseCases.Category.UpdateCategory;
using ExpenseControl.Application.UseCases.Login.DoLogin;
using ExpenseControl.Application.UseCases.Person.CreatePerson;
using ExpenseControl.Application.UseCases.Person.DeletePerson;
using ExpenseControl.Application.UseCases.Person.GetPeoplePaginated;
using ExpenseControl.Application.UseCases.Person.GetPersonById;
using ExpenseControl.Application.UseCases.Person.UpdatePerson;
using ExpenseControl.Application.UseCases.Reports.GetCategoriesBalance;
using ExpenseControl.Application.UseCases.Reports.GetPeopleBalance;
using ExpenseControl.Application.UseCases.Tokens.RefreshToken;
using ExpenseControl.Application.UseCases.Transaction.CreateTransaction;
using ExpenseControl.Application.UseCases.Transaction.DeleteTransactionById;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionById;
using ExpenseControl.Application.UseCases.Transaction.GetTransactionsPaginated;
using ExpenseControl.Application.UseCases.User.DeleteUserById;
using ExpenseControl.Application.UseCases.User.RegisterUser;
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
			.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>()
			.AddScoped<IDeleteCategoryByIdUseCase, DeleteCategoryByIdUseCase>()
			.AddScoped<IGetCategoriesPaginatedUseCase, GetCategoriesPaginatedUseCase>()
			.AddScoped<IGetCategoriesBalanceUseCase, GetCategoriesBalanceUseCase>();

		services
			.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>()
			.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>()
			.AddScoped<IUpdatePersonUseCase, UpdatePersonUseCase>()
			.AddScoped<IGetPersonByIdUseCase, GetPersonByIdUseCase>()
			.AddScoped<IGetPeoplePaginatedUseCase, GetPeoplePaginatedUseCase>()
			.AddScoped<IGetPeopleBalanceUseCase, GetPeopleBalanceUseCase>();

		services
			.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>()
			.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>()
			.AddScoped<IGetTransactionByIdUseCase, GetTransactionByIdUseCase>()
			.AddScoped<IGetTransactionsPaginatedUseCase, GetTransactionsPaginatedUseCase>();

		services
			.AddScoped<IGetCategoriesBalanceUseCase, GetCategoriesBalanceUseCase>()
			.AddScoped<IGetPeopleBalanceUseCase, GetPeopleBalanceUseCase>();

		services
			.AddScoped<ILoginUserUseCase, LoginUserUseCase>()
			.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
			.AddScoped<IDeleteUserByIdUseCase, DeleteUserByIdUseCase>();

		services
			.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

		return services;
	}
}

