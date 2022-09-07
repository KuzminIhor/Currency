using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Enums;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Helpers
{
	public class FinishAuthentication: AbstractAuthenticationHandler, IFinishAuthentication
	{
		public override object Handle(string userName, string password)
		{
			var form = GetFormToRedirect(userName, password);
			return form;
		}

		public FormType GetFormToRedirect(string userName, string password)
		{
			if (userName.Equals("guest"))
			{
				return FormType.GuestForm;
			}
			
			if (userName.Equals("admin"))
			{
				return FormType.AdminForm;
			}

			return FormType.BankUserForm;
		}
	}
}