using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Enums;
using CurrencyApp.Model.Interfaces.Helpers;

namespace CurrencyApp.Helpers
{
	public class FinishAuthentication: AbstractAuthenticationHandler, IFinishAuthentication
	{
		private readonly DBAppContext db;

		public FinishAuthentication(DBAppContext db)
		{
			this.db = db;
		}

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
			} else if (userName.Equals("admin"))
			{
				return FormType.AdminForm;
			}

			return FormType.BankUserForm;
		}
	}
}