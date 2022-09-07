using CurrencyApp.Interfaces;
using CurrencyApp.Model.Enums;

namespace CurrencyApp.Model.Interfaces.Helpers
{
	public interface IFinishAuthentication: IAuthenticationHandler
	{
		public FormType GetFormToRedirect(string userName, string password);
	}
}