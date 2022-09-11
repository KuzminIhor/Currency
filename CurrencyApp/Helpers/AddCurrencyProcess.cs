using CurrencyApp.Core;
using CurrencyApp.Helpers.Interfaces;
using CurrencyApp.Model;
using CurrencyApp.Model.Abstracts;
using CurrencyApp.Repositories.Interfaces;

namespace CurrencyApp.Helpers
{
	public class AddCurrencyProcess: AbstractCurrencyHandler, IAddCurrencyProcess
	{
		private ICurrencyRepository currencyRepository;

		public AddCurrencyProcess()
		{
			currencyRepository = ServiceLocator.Get<ICurrencyRepository>();
		}

		public override void AddCurrencyHandle(string currencyName)
		{
			Add(currencyName);

			base.AddCurrencyHandle(currencyName);
		}

		public void Add(string currencyName)
		{
			currencyRepository.AddCurrency(new Currency() {CurrencyName = currencyName});
		}
	}
}