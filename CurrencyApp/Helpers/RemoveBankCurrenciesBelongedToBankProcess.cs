using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveBankCurrenciesBelongedToBankProcess: AbstractBankHandler, IRemoveBankCurrenciesBelongedToBankProcess
	{
		private readonly IBankCurrencyRepository bankCurrencyRepository;

		public RemoveBankCurrenciesBelongedToBankProcess()
		{
			bankCurrencyRepository = ServiceLocator.Get<IBankCurrencyRepository>();
		}

		public override void RemoveBankHandle(int bankId)
		{
			Remove(bankId);

			base.RemoveBankHandle(bankId);
		}

		public void Remove(int bankId)
		{
			var bankCurrencies = bankCurrencyRepository.GetBankCurrenciesCollection(bankId);

			bankCurrencyRepository.RemoveBankCurrencies(bankCurrencies);
		}
	}
}