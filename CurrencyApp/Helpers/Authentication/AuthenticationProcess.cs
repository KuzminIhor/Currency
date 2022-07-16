using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Interfaces.Helpers;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class AuthenticationProcess: AbstractAuthenticationHandler, IAuthenticationProcess
	{
		private readonly DBAppContext db;
		private IUserRepository UserRepository { get; }

		public AuthenticationProcess(DBAppContext db,
			IUserRepository userRepository)
		{
			this.db = db;
			UserRepository = userRepository;
		}

		public override object Handle(string userName, string password)
		{
			if (userName.Equals("guest"))
			{
				return base.Handle(userName, password);
			}

			Authenticate(userName, password);

			return base.Handle(userName, password);
		}

		public void Authenticate(string userName, string password)
		{
			User user = UserRepository.GetByUserName(userName);
				
			if (user == null)
			{
				throw new AuthenticationException("Такого користувача не існує!!\n");
			}

			if (!UserRepository.IsCorrectPassword(user, password))
			{
				throw new AuthenticationException("Пароль не є вірним!\n");
			}

			CurrentUser currentUser = CurrentUser.GetInstance();
			currentUser.Id = user.Id;
			currentUser.UserName = user.UserName;
			currentUser.IsBankUser = user.IsBankUser;
		}
	}
}