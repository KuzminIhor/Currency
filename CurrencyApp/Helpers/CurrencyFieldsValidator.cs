using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Model.Exceptions;

namespace CurrencyApp.Helpers
{
	public class CurrencyFieldsValidator: AbstractCurrencyHandler, ICurrencyFieldsValidator
	{
		public override void AddCurrencyHandle(string currencyName)
		{
			ValidateCurrencyName(currencyName);

			base.AddCurrencyHandle(currencyName);
		}

		public override void UpdateCurrencyHandle(int currencyId, string currencyName)
		{
			ValidateCurrencyName(currencyName);

			base.UpdateCurrencyHandle(currencyId, currencyName);
		}

		public void ValidateCurrencyName(string currencyName)
		{
			if (string.IsNullOrEmpty(currencyName))
			{
				throw new CurrencyModifyException("Ви не ввели валюту!");
			}
		}
	}
}