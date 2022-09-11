using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IUserFieldsValidator: IUserHandler
	{
		public void ValidateFirstName(string firstName);
		public void ValidateLastName(string lastName);
		public void ValidateBank(Bank bank);
	}
}