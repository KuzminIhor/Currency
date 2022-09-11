using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IBankCurrencyHandler
	{
		public IBankCurrencyHandler SetNext(IBankCurrencyHandler handler);
		public object AddBankCurrencyHandle(Currency currency, string convertation);
		public void UpdateBankCurrencyHandle(int bankCurrencyId, string convertation);
	}
}