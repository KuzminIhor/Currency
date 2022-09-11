using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUserHandler
	{
		public IUserHandler SetNext(IUserHandler handler);
		public void AddUserHandle(string firstName, string lastName, Bank bank);
		public void UpdateUserHandle(int userId, string firstName, string lastName, Bank bank);
		public void RemoveUserHandle(int userId);
	}
}