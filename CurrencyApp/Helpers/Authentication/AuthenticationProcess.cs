using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;

namespace CurrencyApp.Helpers
{
	public class AuthenticationProcess: AbstractAuthenticationHandler
	{
		private readonly DBAppContext db;

		public AuthenticationProcess()
		{
			db = ServiceLocator.Get<DBAppContext>();
		}

		public override object Handle(string userName, string password)
		{
			if (userName.Equals("guest"))
			{
				return base.Handle(userName, password);
			}

			User user = db.Users.FirstOrDefault(u => u.UserName.Equals(userName));

			if (user == null)
			{
				throw new AuthenticationException("Такого користувача не існує!!\n");
			}

			if (!user.Password.Equals(password))
			{
				throw new AuthenticationException("Пароль не є вірним!\n");
			}

			CurrentUser currentUser = CurrentUser.GetInstance();
			currentUser.Id = user.Id;
			currentUser.UserName = user.UserName;
			currentUser.IsBankUser = user.IsBankUser;

			return base.Handle(userName, password);
		}
	}
}