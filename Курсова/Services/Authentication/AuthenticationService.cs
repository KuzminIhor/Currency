using System.Windows.Forms;
using CurrencyApp.Helpers;
using CurrencyApp.Interfaces;

namespace Курсова.Services
{
	public class AuthenticationService
	{
		public static object Authenticate(string userName, string password)
		{
			var validator = new AuthenticationFieldsValidator();
			var authProcess = new AuthenticationProcess();
			var finishAuth = new FinishAuthentication();
			
			validator.SetNext(authProcess).SetNext(finishAuth);

			var formToRedirect = (Form) validator.Handle(userName, password);

			return formToRedirect;
		}

		public static object AuthenticateGuest()
		{
			var authProcess = new AuthenticationProcess();
			var finishAuth = new FinishAuthentication();

			authProcess.SetNext(finishAuth);

			var formToRedirect = (Form) authProcess.Handle("guest", string.Empty);

			return formToRedirect;
		}
	}
}