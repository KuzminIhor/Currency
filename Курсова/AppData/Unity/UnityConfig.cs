using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Abstracts;
using Unity;
using Курсова.Services;

namespace Курсова.AppData.Unity
{
	public class UnityConfig
	{
		public static IUnityContainer Container => ServiceLocator.Container;

		public static void Register()
		{
			//Register Core
			ServiceLocator.RegisterSingleton<DBAppContext, DBAppContext>();

			//Register services
			ServiceLocator.RegisterSingleton<IAuthenticationService, AuthenticationService>();
			ServiceLocator.RegisterSingleton<IFormRedirection, FormRedirection>();

			//Register helpers
			ServiceLocator.RegisterSingleton<AbstractAuthenticationHandler, AuthenticationFieldsValidator>(nameof(AuthenticationFieldsValidator));
			ServiceLocator.RegisterSingleton<AbstractAuthenticationHandler, AuthenticationProcess>(nameof(AuthenticationProcess));
			ServiceLocator.RegisterSingleton<AbstractAuthenticationHandler, FinishAuthentication>(nameof(FinishAuthentication));
		}
	}
}
