using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;

namespace CurrencyApp.Helpers
{
	public class FinishAuthentication: AbstractAuthenticationHandler
	{
		public override object Handle(string userName, string password)
		{
			Form form = GuestForm.GetInstance();

			if (userName.Equals("guest"))
			{
				return form;
			}

			using (DBAppContext db = new DBAppContext())
			{
				CurrentUser currentUser = CurrentUser.GetInstance();

				if (currentUser.UserName.Equals("admin"))
				{
					form = AdminForm.GetInstance();
				}
				else if (currentUser.IsBankUser)
				{
					form = BankUserForm.GetInstance();
				}
			}

			return form;
		}
	}
}