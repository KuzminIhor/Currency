using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Services
{
	public class AuthenticationService: IAuthenticationService
	{
		private readonly IAuthenticationFieldsValidator validator;
		private readonly IAuthenticationProcess authProcess;
		private readonly IFinishAuthentication finishAuth;

		public AuthenticationService()
		{
			validator = ServiceLocator.Get<IAuthenticationFieldsValidator>();
			authProcess = ServiceLocator.Get<IAuthenticationProcess>();
			finishAuth = ServiceLocator.Get<IFinishAuthentication>();
		}

		public object Authenticate(string userName, string password)
		{
			validator.SetNext(authProcess).SetNext(finishAuth);

			var formToRedirect = validator.Handle(userName, password) as Form;

			return formToRedirect;
		}

		public object AuthenticateGuest()
		{
			authProcess.SetNext(finishAuth);

			var formToRedirect = authProcess.Handle("guest", string.Empty) as Form;

			return formToRedirect;
		}
	}
}