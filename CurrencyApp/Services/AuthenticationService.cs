using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;
using CurrencyApp.Model.Abstracts;

namespace CurrencyApp.Services
{
	public class AuthenticationService: IAuthenticationService
	{
		private readonly AbstractAuthenticationHandler validator;
		private readonly AbstractAuthenticationHandler authProcess;
		private readonly AbstractAuthenticationHandler finishAuth;

		public AuthenticationService()
		{
			validator = ServiceLocator.Get<AbstractAuthenticationHandler>(nameof(AuthenticationFieldsValidator));
			authProcess = ServiceLocator.Get<AbstractAuthenticationHandler>(nameof(AuthenticationProcess));
			finishAuth = ServiceLocator.Get<AbstractAuthenticationHandler>(nameof(FinishAuthentication));
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