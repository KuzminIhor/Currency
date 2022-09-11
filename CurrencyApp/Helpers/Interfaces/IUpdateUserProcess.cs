using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUpdateUserProcess: IUserHandler
	{
		public void Update(int userId, string firstName, string lastName, Bank bank);
	}
}