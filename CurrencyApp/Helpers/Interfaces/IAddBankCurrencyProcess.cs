using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IAddBankCurrencyProcess: IBankCurrencyHandler
	{
		public void Add(Currency currency, string convertation);
	}
}