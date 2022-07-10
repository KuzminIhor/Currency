using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Helpers
{
	public class FinishAuthentication: AbstractAuthenticationHandler, IFinishAuthentication
	{
		private readonly DBAppContext db;

		public FinishAuthentication()
		{
			db = ServiceLocator.Get<DBAppContext>();
		}

		public override object Handle(string userName, string password)
		{
			var form = GetFormToRedirect(userName, password);
			return form;
		}

		public Form GetFormToRedirect(string userName, string password)
		{
			Form form = GuestForm.GetInstance();

			if (userName.Equals("guest"))
			{
				return form;
			}

			CurrentUser currentUser = CurrentUser.GetInstance();

			if (currentUser.UserName.Equals("admin"))
			{
				form = AdminForm.GetInstance();
			}
			else if (currentUser.IsBankUser)
			{
				form = BankUserForm.GetInstance();
			}

			return form;
		}
	}
}