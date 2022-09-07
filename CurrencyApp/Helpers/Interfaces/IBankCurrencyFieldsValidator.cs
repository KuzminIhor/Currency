using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IBankCurrencyFieldsValidator: IAddBankCurrencyHandler
	{
		public void ValidateCurrency(Currency currency);
		public void ValidateConvertation(string convertation);
	}
}