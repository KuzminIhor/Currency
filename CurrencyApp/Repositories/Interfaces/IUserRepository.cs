using System.Collections.Generic;
using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IUserRepository
	{
		public User GetByUserName(string username);
		public List<User> GetUsersExceptAdminAndGuest();
		public bool IsCorrectPassword(int userId, string password);
	}
}