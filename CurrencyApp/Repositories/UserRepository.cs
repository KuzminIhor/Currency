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
		public readonly DBAppContext db;

		public UserRepository(DBAppContext dbApp)
		{
			this.db = dbApp;
		}

		public User GetByUserName(string username)
		{
			return db.Users.FirstOrDefault(u => u.UserName.Equals(username));
		}

		public List<User> GetUsersCollection(int bankId)
		{
			return db.Users.Include(bc => bc.Bank)
				.Where(bc => bc.Bank.Id == bankId).ToList();
		}

		public List<User> GetUsersCollectionExceptAdminAndGuest()
		{
			return db.Users.Include(u => u.Bank).Where(u => u.Id > 2).ToList();
		}

		public bool IsCorrectPassword(int userId, string password)
		{
			var user = db.Users.FirstOrDefault(u => u.Id == userId);
			return user.Password.Equals(password);
		}

		public void RemoveUsers(List<User> users)
		{
			db.Users.RemoveRange(users);
			db.SaveChanges();
		}
	}
}