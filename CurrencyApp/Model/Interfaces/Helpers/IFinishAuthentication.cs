using System.Windows.Forms;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Model.Interfaces.Helpers
{
	public interface IFinishAuthentication: IAuthenticationHandler
	{
		public Form GetFormToRedirect(string userName, string password);
	}
}