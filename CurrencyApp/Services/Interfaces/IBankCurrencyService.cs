using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IBankCurrencyService
	{
		public object AddBankCurrency(Currency currency, string convertation);
		public void UpdateBankCurrency(int bankCurrencyId, string convertation);
		public void RemoveBankCurrency(int bankCurrencyId);
	}
}