using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IAddBankCurrencyService
	{
		public object AddBankCurrency(Currency currency, string convertation);
	}
}