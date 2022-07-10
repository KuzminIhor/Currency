using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Interfaces.Helpers
{
	public interface IAuthenticationFieldsValidator: IAuthenticationHandler
	{
		public void Validate(string userName, string password);
	}
}