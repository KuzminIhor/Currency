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
			ServiceLocator.RegisterSingleton<IFormRedirectionService, FormRedirectionService>();


			//Register helpers
			ServiceLocator.RegisterSingleton<IAuthenticationFieldsValidator, AuthenticationFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAuthenticationProcess, AuthenticationProcess>();
			ServiceLocator.RegisterSingleton<IFinishAuthentication, FinishAuthentication>();

			ServiceLocator.RegisterSingleton<IBankCurrencyFieldsValidator, BankCurrencyFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAddBankCurrencyProcess, AddBankCurrencyProcess>();


			//Register repositories
			ServiceLocator.RegisterSingleton<IUserRepository, UserRepository>();
			ServiceLocator.RegisterSingleton<IBankCurrencyRepository, BankCurrencyRepository>();
			ServiceLocator.RegisterSingleton<ICurrencyRepository, CurrencyRepository>();
			ServiceLocator.RegisterSingleton<IBankRepository, BankRepository>();
		}
	}
}
