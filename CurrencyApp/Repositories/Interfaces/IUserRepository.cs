using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IUserRepository
	{
		public User GetUser(string username);
		public User GetUser(int userId);
		public List<User> GetUsersCollection(int bankId);
		public List<User> GetUsersCollectionExceptAdminAndGuest();
		public bool IsCorrectPassword(int userId, string password);
		public void AddUser(User user);
		public void UpdateUser(User user);
		public void RemoveUsers(params User[] users);
	}
}