using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IBankCurrencyService
	{
		public object AddBankCurrency(Currency currency, string convertation);
		public void UpdateBankCurrency(string convertation, int bankCurrencyId);
		public void RemoveBankCurrency(int bankCurrencyId);
	}
}