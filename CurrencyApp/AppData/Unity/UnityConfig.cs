using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Abstracts;
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
			ServiceLocator.RegisterSingleton<IFormRedirection, FormRedirection>();

			//Register helpers
			ServiceLocator.RegisterSingleton<IAuthenticationFieldsValidator, AuthenticationFieldsValidator>();
			ServiceLocator.RegisterSingleton<IAuthenticationProcess, AuthenticationProcess>();
			ServiceLocator.RegisterSingleton<IFinishAuthentication, FinishAuthentication>();

			//Register repositories
			ServiceLocator.RegisterSingleton<IUserRepository, UserRepository>();
			ServiceLocator.RegisterSingleton<IBankCurrenciesRepository, BankCurrenciesRepository>();
			ServiceLocator.RegisterSingleton<ICurrenciesRepository, CurrenciesRepository>();
			ServiceLocator.RegisterSingleton<IBankRepository, BankRepository>();
		}
	}
}
