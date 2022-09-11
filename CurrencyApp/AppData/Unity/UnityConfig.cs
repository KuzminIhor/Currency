using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Interfaces.Helpers;
using CurrencyApp.Repositories;
using CurrencyApp.Repositories.Interfaces;
using Unity;
using CurrencyApp.Services;

namespace CurrencyApp.AppData.Unity
{
	public class UnityConfig
	{
		public static IUnityContainer Container => ServiceLocator.Container;

		public static void Register()
		{
			//Register Core
			var dbApp = new DBAppContext();
			ServiceLocator.RegisterSingleton(dbApp);


			//Register services
			ServiceLocator.RegisterSingleton<IAuthenticationService, AuthenticationService>();
			ServiceLocator.RegisterSingleton<IBankCurrencyService, BankCurrencyService>();
			ServiceLocator.RegisterSingleton<IBankService, BankService>();
			ServiceLocator.RegisterSingleton<ICurrencyService, CurrencyService>();
			ServiceLocator.RegisterSingleton<IUserService, UserService>();
			ServiceLocator.RegisterSingleton<IFormRedirectionService, FormRedirectionService>();

			ServiceLocator.RegisterSingleton<IRenderDataTableRows, RenderUserDataTableRowsService>(nameof(RenderUserDataTableRowsService));
			ServiceLocator.RegisterSingleton<IRenderDataTableRows, RenderCurrencyDataTableRowsService>(nameof(RenderCurrencyDataTableRowsService));
			ServiceLocator.RegisterSingleton<IRenderDataTableRows, RenderBankDataTableRowsService>(nameof(RenderBankDataTableRowsService));


			//Register helpers
			ServiceLocator.RegisterSingleton<IAuthenticationFieldsValidator, AuthenticationFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAuthenticationProcess, AuthenticationProcess>();
			ServiceLocator.RegisterSingleton<IFinishAuthentication, FinishAuthentication>();

			ServiceLocator.RegisterSingleton<IBankCurrencyFieldsValidator, BankCurrencyFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAddBankCurrencyProcess, AddBankCurrencyProcess>();
			ServiceLocator.RegisterSingleton<IUpdateBankCurrencyProcess, UpdateBankCurrencyProcess>();

			ServiceLocator.RegisterSingleton<IBankFieldsValidator, BankFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAddBankProcess, AddBankProcess>();
			ServiceLocator.RegisterSingleton<IUpdateBankProcess, UpdateBankProcess>();
			ServiceLocator.RegisterSingleton<IRemoveBankProcess, RemoveBankProcess>();
			ServiceLocator.RegisterSingleton<IRemoveUsersWorkingInBankProcess, RemoveUsersWorkingInBankProcess>();
			ServiceLocator.RegisterSingleton<IRemoveBankCurrenciesBelongedToBankProcess, RemoveBankCurrenciesBelongedToBankProcess>();

			ServiceLocator.RegisterSingleton<ICurrencyFieldsValidator, CurrencyFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAddCurrencyProcess, AddCurrencyProcess>();
			ServiceLocator.RegisterSingleton<IUpdateCurrencyProcess, UpdateCurrencyProcess>();
			ServiceLocator.RegisterSingleton<IRemoveCurrencyProcess, RemoveCurrencyProcess>();
			ServiceLocator.RegisterSingleton<IRemoveBankCurrenciesBelongedToCurrencyProcess, RemoveBankCurrenciesBelongedToCurrencyProcess>();

			ServiceLocator.RegisterSingleton<IUserFieldsValidator, UserFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAddUserProcess, AddUserProcess>();
			ServiceLocator.RegisterSingleton<IUpdateUserProcess, UpdateUserProcess>();
			ServiceLocator.RegisterSingleton<IRemoveUserProcess, RemoveUserProcess>();


			//Register repositories
			ServiceLocator.RegisterSingleton<IUserRepository, UserRepository>();
			ServiceLocator.RegisterSingleton<IBankCurrencyRepository, BankCurrencyRepository>();
			ServiceLocator.RegisterSingleton<ICurrencyRepository, CurrencyRepository>();
			ServiceLocator.RegisterSingleton<IBankRepository, BankRepository>();
		}
	}
}
