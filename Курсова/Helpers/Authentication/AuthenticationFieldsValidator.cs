using System.Security.Authentication;
using CurrencyApp.Model.Abstracts;

namespace CurrencyApp.Helpers
{
	public class AuthenticationFieldsValidator: AbstractAuthenticationHandler
	{
		public override object Handle(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new AuthenticationException("Ви не ввели логін!\n");
			}

			if (string.IsNullOrEmpty(password))
			{
				throw new AuthenticationException("Ви не ввели пароль!\n");
			}

			return base.Handle(userName, password);
		}
	}
}