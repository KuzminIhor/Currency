using CurrencyApp.Model;

namespace CurrencyApp.Interfaces
{
	public interface IAddBankCurrencyHandler
	{
		public IAddBankCurrencyHandler SetNext(IAddBankCurrencyHandler handler);
		public object Handle(Currency currency, string convertation);
	}
}