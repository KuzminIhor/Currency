using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Interfaces.Helpers
{
	public interface IAuthenticationFieldsValidator: IAuthenticationHandler
	{
		public void ValidateUserName(string userName);
		public void ValidatePassword(string password);
	}
}