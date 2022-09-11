using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IAddUserProcess: IUserHandler
	{
		public void Add(string firstName, string lastName, Bank bank);
	}
}