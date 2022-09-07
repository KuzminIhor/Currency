using System.Security.Authentication;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Helpers
{
	public class AuthenticationFieldsValidator: AbstractAuthenticationHandler, IAuthenticationFieldsValidator
	{
		public override object Handle(string userName, string password)
		{
			ValidateUserName(userName);
			ValidatePassword(password);

			return base.Handle(userName, password);
		}

		public void ValidateUserName(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new AuthenticationException("Ви не ввели логін!\n");
			}
		}

		public void ValidatePassword(string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new AuthenticationException("Ви не ввели пароль!\n");
			}
		}
	}
}