using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IBankCurrencyFieldsValidator: IAddBankCurrencyHandler
	{
		public void Validate(Currency currency, string convertation);
	}
}