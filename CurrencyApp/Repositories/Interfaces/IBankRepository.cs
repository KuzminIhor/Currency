using CurrencyApp.Model;

namespace CurrencyApp.Repositories.Interfaces
{
	public interface IBankRepository
	{
		public Bank GetBankByUserId(int userId);
	}
}