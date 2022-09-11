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

		public User GetUser(string username)
		{
			return db.Users.FirstOrDefault(u => u.UserName.Equals(username));
		}

		public User GetUser(int userId)
		{
			return db.Users.Include(p => p.Bank).ToList().FirstOrDefault(p => p.Id == userId);
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

		public void AddUser(User user)
		{
			db.Users.Add(user);
			db.SaveChanges();
		}

		public void UpdateUser(User user)
		{
			db.Users.Update(user);
			db.SaveChanges();
		}

		public void RemoveUsers(params User[] users)
		{
			db.Users.RemoveRange(users);
			db.SaveChanges();
		}
	}
}