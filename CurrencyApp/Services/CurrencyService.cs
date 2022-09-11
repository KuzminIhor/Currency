using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Interfaces;

namespace CurrencyApp.Services
{
	public class CurrencyService: ICurrencyService
	{
		private readonly ICurrencyFieldsValidator validator;
		private readonly IAddCurrencyProcess addCurrencyProcess;
		private readonly IUpdateCurrencyProcess updateCurrencyProcess;
		private readonly IRemoveCurrencyProcess removeCurrencyProcess;
		private readonly IRemoveBankCurrenciesBelongedToCurrencyProcess removeBankCurrenciesBelongedToCurrencyProcess;

		public CurrencyService()
		{
			validator = ServiceLocator.Get<ICurrencyFieldsValidator>();
			addCurrencyProcess = ServiceLocator.Get<IAddCurrencyProcess>();
			updateCurrencyProcess = ServiceLocator.Get<IUpdateCurrencyProcess>();

			removeCurrencyProcess = ServiceLocator.Get<IRemoveCurrencyProcess>();
			removeBankCurrenciesBelongedToCurrencyProcess =
				ServiceLocator.Get<IRemoveBankCurrenciesBelongedToCurrencyProcess>();
		}

		public void AddCurrency(string currencyName)
		{
			validator.SetNext(addCurrencyProcess);

			validator.AddCurrencyHandle(currencyName);
		}

		public void UpdateCurrency(int currencyId, string currencyName)
		{
			validator.SetNext(updateCurrencyProcess);

			validator.UpdateCurrencyHandle(currencyId, currencyName);
		}

		public void RemoveCurrency(int currencyId)
		{
			removeBankCurrenciesBelongedToCurrencyProcess.SetNext(removeCurrencyProcess);

			removeBankCurrenciesBelongedToCurrencyProcess.RemoveCurrencyHandle(currencyId);
		}
	}
}