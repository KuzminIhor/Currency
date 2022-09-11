using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class RemoveCurrencyProcess: AbstractCurrencyHandler, IRemoveCurrencyProcess
	{
		private ICurrencyRepository currencyRepository;

		public RemoveCurrencyProcess()
		{
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
		}

		public override void RemoveCurrencyHandle(int currencyId)
		{
			Remove(currencyId);

			base.RemoveCurrencyHandle(currencyId);
		}

		public void Remove(int currencyId)
		{
			var currency = currencyRepository.GetCurrency(currencyId);

			currencyRepository.RemoveCurrency(currency);
		}
	}
}