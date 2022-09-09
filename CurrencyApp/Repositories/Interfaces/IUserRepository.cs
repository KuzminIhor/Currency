using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IUserRepository
	{
		public User GetByUserName(string username);
		public List<User> GetUsersCollection(int bankId);
		public List<User> GetUsersCollectionExceptAdminAndGuest();
		public bool IsCorrectPassword(int userId, string password);
		public void RemoveUsers(List<User> users);
	}
}