using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IUserService
	{
		public void AddUser(string firstName, string lastName, Bank bank);
		public void UpdateUser(int userId, string firstName, string lastName, Bank bank);
		public void RemoveUser(int userId);
	}
}