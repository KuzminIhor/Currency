using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Interfaces.Helpers
{
	public interface IAuthenticationProcess: IAuthenticationHandler
	{
		public void Authenticate(string userName, string password);
	}
}