using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveBankCurrenciesBelongedToCurrencyProcess: AbstractCurrencyHandler, IRemoveBankCurrenciesBelongedToCurrencyProcess
	{
		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public RemoveBankCurrenciesBelongedToCurrencyProcess()
		{
			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public override void RemoveCurrencyHandle(int currencyId)
		{
			Remove(currencyId);

			base.RemoveCurrencyHandle(currencyId);
		}

		public void Remove(int currencyId)
		{
			var bankCurrencies = bankCurrencyRepository.GetBankCurrenciesCollectionByCurrency(currencyId);

			bankCurrencyRepository.RemoveBankCurrencies(bankCurrencies);
		}
	}
}