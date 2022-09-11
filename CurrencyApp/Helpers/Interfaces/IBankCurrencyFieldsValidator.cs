using CurrencyApp.Interfaces;
using CurrencyApp.Model;

namespace CurrencyApp.Helpers.Interfaces
{
	public interface IBankCurrencyFieldsValidator: IBankCurrencyHandler
	{
		public void ValidateCurrency(Currency currency);
		public void ValidateConvertation(string convertation);
	}
}