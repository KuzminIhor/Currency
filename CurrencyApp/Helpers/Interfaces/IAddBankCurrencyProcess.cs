using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IAddBankCurrencyProcess: IAddBankCurrencyHandler
	{
		public void Add(Currency currency, string convertation);
	}
}