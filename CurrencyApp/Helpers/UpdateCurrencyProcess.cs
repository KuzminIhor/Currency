using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class UpdateCurrencyProcess: AbstractCurrencyHandler, IUpdateCurrencyProcess
	{
		private ICurrencyRepository currencyRepository;

		public UpdateCurrencyProcess()
		{
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
		}

		public override void UpdateCurrencyHandle(int currencyId, string currencyName)
		{
			Update(currencyId, currencyName);

			base.UpdateCurrencyHandle(currencyId, currencyName);
		}

		public void Update(int currencyId, string currencyName)
		{
			var currency = currencyRepository.GetCurrency(currencyId);

			currency.CurrencyName = currencyName;

			currencyRepository.UpdateCurrency(currency);
		}
	}
}