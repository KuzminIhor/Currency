using System.Collections.Generic;
using System.Linq;
using CurrencyApp.Core;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Repositories
{
	public class UserRepository: IUserRepository
	{
		public DBAppContext db { get; private set; }

		public UserRepository(DBAppContext db)
		{
			this.db = db;
		}

		public User GetByUserName(string username)
		{
;			return db.Users.FirstOrDefault(u => u.UserName.Equals(username));
		}

		public List<User> GetUsersExceptAdminAndGuest()
		{
			return db.Users.Include(u => u.Bank).Where(u => u.Id > 2).ToList();
		}

		public bool IsCorrectPassword(User user, string password)
		{
			return user.Password.Equals(password);
		}
	}
}